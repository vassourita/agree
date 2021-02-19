using System.Linq;
using System;
using System.Threading.Tasks;
using Agree.Athens.Application.Dtos.Account;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Infrastructure.Configuration;
using Agree.Athens.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Agree.Athens.Application.Services
{
    public class AuthService
    {
        private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IdentityContext identityContext;

        public AuthService(IOptions<JwtBearerTokenSettings> jwtTokenOptions, UserManager<ApplicationUser> userManager, IdentityContext identityContext)
        {
            _jwtBearerTokenSettings = jwtTokenOptions.Value;
            this.userManager = userManager;
            this.identityContext = identityContext;
        }

        public async Task Register(CreateAccountDto createAccountDto)
        {
            if (createAccountDto == null)
            {
                throw new NullReferenceException(nameof(createAccountDto));
            }

            var userAccount = new ApplicationUser() { UserName = createAccountDto.Username, Email = createAccountDto.Email };
            var result = await userManager.CreateAsync(userAccount, createAccountDto.Password);
            if (!result.Succeeded)
            {
                throw AccountException.RegisterError(result.Errors.Select(e => e.Description));
            }
        }

        public async Task<string> Login(LoginDto credentials, string ipAdress)
        {
            ApplicationUser identityUser;

            if (credentials == null || (identityUser = await ValidateUser(credentials)) == null)
            {
                throw AccountException.LoginError();
            }

            var token = GenerateTokens(identityUser, ipAdress);
            return token;
        }

        public string RefreshToken(string token, string ipAddress)
        {
            var identityUser = identityContext.Users.Include(x => x.RefreshTokens)
                .FirstOrDefault(x => x.RefreshTokens.Any(y => y.Token == token && y.UserId == x.Id));

            var existingRefreshToken = GetValidRefreshToken(token, identityUser);
            if (existingRefreshToken == null)
            {
                throw AccountException.NoRefreshTokens();
            }

            existingRefreshToken.RevokedByIp = ipAddress;
            existingRefreshToken.RevokedOn = DateTime.UtcNow;

            var newToken = GenerateTokens(identityUser, ipAddress);
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

        private RefreshToken GetValidRefreshToken(string token, ApplicationUser identityUser)
        {
            if (identityUser == null)
            {
                return null;
            }

            var existingToken = identityUser.RefreshTokens.FirstOrDefault(x => x.Token == token);
            return IsRefreshTokenValid(existingToken) ? existingToken : null;
        }

        private async Task<bool> RevokeRefreshToken(string token, string revokedByIp)
        {
            var identityUser = await identityContext.Users.Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == token && y.UserId == x.Id));
            if (identityUser == null)
            {
                return false;
            }

            var existingToken = identityUser.RefreshTokens.FirstOrDefault(x => x.Token == token);
            existingToken.RevokedByIp = revokedByIp;
            existingToken.RevokedOn = DateTime.UtcNow;
            identityContext.Update(identityUser);
            await identityContext.SaveChangesAsync();
            return true;
        }

        private async Task<ApplicationUser> ValidateUser(LoginDto loginDto)
        {
            var identityUser = await userManager.FindByEmailAsync(loginDto.Email);
            if (identityUser != null)
            {
                var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, loginDto.Password);
                return result == PasswordVerificationResult.Failed ? null : identityUser;
            }

            return null;
        }

        private string GenerateTokens(ApplicationUser identityUser, string ipAddress)
        {
            var accessToken = GenerateAccessToken(identityUser);

            var refreshToken = GenerateRefreshToken(ipAddress, identityUser.Id);

            if (identityUser.RefreshTokens == null)
            {
                identityUser.RefreshTokens = new List<RefreshToken>();
            }

            identityUser.RefreshTokens.Add(refreshToken);
            identityContext.Update(identityUser);
            identityContext.SaveChanges();
            return accessToken;
        }

        private string GenerateAccessToken(ApplicationUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, identityUser.Email)
                }),

                Expires = DateTime.UtcNow.AddSeconds(_jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerTokenSettings.Audience,
                Issuer = _jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress, string userId)
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