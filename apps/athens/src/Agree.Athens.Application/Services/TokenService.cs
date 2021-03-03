using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Agree.Athens.Infrastructure.Configuration;
using Agree.Athens.Domain.Aggregates.Account;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Agree.Athens.Application.Security;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Exceptions;

namespace Agree.Athens.Application.Services
{
    public class TokenService
    {
        public readonly JwtConfiguration _jwtConfiguration;
        public readonly ITokenRepository _tokenRepository;
        public readonly IAccountRepository _accountRepository;

        public TokenService(IOptions<JwtConfiguration> jwtConfiguration,
                            ITokenRepository tokenRepository,
                            IAccountRepository accountRepository)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _tokenRepository = tokenRepository;
            _accountRepository = accountRepository;
        }

        public AccessToken GenerateAccessToken(UserAccount account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key);

            var expiresIn = DateTime.UtcNow.AddHours(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.UserNameWithTag),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim("id", account.Id.ToString())
                }),
                Expires = expiresIn,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _jwtConfiguration.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AccessToken
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = expiresIn.Subtract(DateTime.UtcNow).Ticks,
                Type = "Bearer"
            };
        }

        public RefreshToken GenerateRefreshToken(string ipAddress, Guid userId)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    ExpiryOn = DateTime.UtcNow.AddDays(30),
                    CreatedOn = DateTime.UtcNow,
                    CreatedByIp = ipAddress,
                    UserId = userId
                };
            }
        }

        public async Task<(AccessToken, RefreshToken)> RefreshTokens(string refreshToken, string ipAddress)
        {
            var token = await _tokenRepository.GetAsync(refreshToken);
            if (token == null)
            {
                throw DomainUnauthorizedException.InvalidRefreshToken();
            }
            else if (token.ExpiryOn < DateTime.UtcNow)
            {
                throw DomainUnauthorizedException.ExpiredRefreshToken();
            }

            var account = await _accountRepository.GetByIdAsync(token.UserId);
            if (account == null)
            {
                throw DomainUnauthorizedException.AccountDisabled();
            }

            return (GenerateAccessToken(account), GenerateRefreshToken(ipAddress, account.Id));
        }
    }
}