using System.Linq;
using System;
using System.Threading.Tasks;
using Agree.Athens.Application.Dtos.Auth;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Entities;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Domain.Security;
using Agree.Athens.Domain.Specifications;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Application.Services
{
    public class AuthService
    {
        private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashProvider _hashProvider;
        private readonly UserService _userService;

        public AuthService(IOptions<JwtBearerTokenSettings> jwtTokenOptions,
                           IBaseRepository<User> userRepository,
                           IUnitOfWork unitOfWork,
                           IHashProvider hashProvider,
                           UserService userService)
        {
            _jwtBearerTokenSettings = jwtTokenOptions.Value;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _hashProvider = hashProvider;
            _userService = userService;
        }

        public async Task<Guid> Register(CreateAccountDto createAccountDto)
        {
            var emailAlreadyInUse = await _userService.IsUniqueEmail(createAccountDto.Email);
            if (emailAlreadyInUse)
            {
                throw AccountException.EmailAlreadyInUse(createAccountDto.Email);
            }

            var tag = await _userService.GenerateUniqueTagForUser(createAccountDto.Username);
            var passwordHash = _hashProvider.Hash(createAccountDto.Password);
            var user = new User(createAccountDto.Username, createAccountDto.Email, tag, passwordHash);
            await _userRepository.AddAsync(user);

            return user.Id;
        }

        public async Task<string> Login(LoginDto loginDto, string ipAdress)
        {
            User user;

            if (loginDto == null || (user = await ValidateUser(loginDto)) == null)
            {
                throw AccountException.LoginError();
            }

            var token = await GenerateTokens(user, ipAdress);
            return token;
        }

        public async Task<string> RefreshToken(string token, string ipAddress)
        {
            var user = await _userRepository.GetBySpecAsync(new RefreshTokenSpecification(token));

            var existingRefreshToken = GetValidRefreshToken(token, user);
            if (existingRefreshToken == null)
            {
                throw AccountException.NoRefreshTokens();
            }

            existingRefreshToken.RevokedByIp = ipAddress;
            existingRefreshToken.RevokedOn = DateTime.UtcNow;

            var newToken = await GenerateTokens(user, ipAddress);
            return newToken;
        }

        public async Task RevokeToken(string token, string revokedByIp)
        {
            if (!await RevokeRefreshToken(token, revokedByIp))
            {
                throw AccountException.CannotRevokeRefreshToken();
            }
        }

        public async Task Logout(string token, string revokedByIp)
        {
            await RevokeRefreshToken(token, revokedByIp);
        }

        private RefreshToken GetValidRefreshToken(string token, User user)
        {
            if (user == null)
            {
                return null;
            }

            var existingToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);
            return IsRefreshTokenValid(existingToken) ? existingToken : null;
        }

        private async Task<bool> RevokeRefreshToken(string token, string revokedByIp)
        {
            var user = await _userRepository.GetBySpecAsync(new RefreshTokenSpecification(token));

            if (user == null)
            {
                return false;
            }

            var existingToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);
            existingToken.RevokedByIp = revokedByIp;
            existingToken.RevokedOn = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.Commit();
            return true;
        }

        private async Task<User> ValidateUser(LoginDto loginDto)
        {
            var user = await _userRepository.GetBySpecAsync(new UserEmailSpecification(loginDto.Email));
            if (user == null)
            {
                return null;
            }
            var compareResult = _hashProvider.Compare(user.PasswordHash, loginDto.Password);
            return compareResult ? user : null;
        }

        private async Task<string> GenerateTokens(User user, string ipAddress)
        {
            var accessToken = GenerateAccessToken(user);

            var refreshToken = GenerateRefreshToken(ipAddress, user.Id);

            user.RefreshTokens.Add(refreshToken);
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.Commit();
            return accessToken;
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                }),

                Expires = DateTime.UtcNow.AddSeconds(_jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerTokenSettings.Audience,
                Issuer = _jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress, Guid userId)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    ExpiryOn = DateTime.UtcNow.AddDays(_jwtBearerTokenSettings.RefreshTokenExpiryInDays),
                    CreatedOn = DateTime.UtcNow,
                    CreatedByIp = ipAddress,
                    UserId = userId
                };
            }
        }

        private bool IsRefreshTokenValid(RefreshToken existingToken)
        {
            // Is token already revoked, then return false
            if (existingToken.RevokedByIp != null && existingToken.RevokedOn != DateTime.MinValue)
            {
                return false;
            }

            // Token already expired, then return false
            if (existingToken.ExpiryOn <= DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }
    }
}