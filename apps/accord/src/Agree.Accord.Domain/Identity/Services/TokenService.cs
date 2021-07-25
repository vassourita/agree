using System;
using System.Text;
using Agree.Accord.Domain.Identity.Tokens;
using Agree.Accord.SharedKernel.Data;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Agree.Accord.Domain.Identity.Specifications;

namespace Agree.Accord.Domain.Identity.Services
{
    public class TokenService
    {
        public readonly JwtConfiguration _jwtConfiguration;
        public readonly IRepository<ApplicationUser> _accountRepository;

        public TokenService(IOptions<JwtConfiguration> jwtConfiguration,
                            IRepository<ApplicationUser> accountRepository)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _accountRepository = accountRepository;
        }

        private AccessToken GenerateTokenCore(ApplicationUser account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.SigningKey);

            var expiresIn = DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpiresInMinutes);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, account.NameTag),
                        new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.Role, "user"),
                        new Claim("verified", account.EmailConfirmed.ToString().ToLower()),
                }),
                Issuer = _jwtConfiguration.Issuer,
                Audience = _jwtConfiguration.Audience,
                Expires = expiresIn,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return new AccessToken
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = ((DateTimeOffset)expiresIn).ToUnixTimeSeconds(),
                Type = "Bearer"
            };
        }

        public Task<AccessToken> GenerateAccessTokenAsync(ApplicationUser account)
        {
            return Task.Run(() => GenerateTokenCore(account));
        }

        public async Task<AccessToken> GenerateAccessTokenAsync(string accountEmail)
        {
            var account = await _accountRepository.GetFirstAsync(new EmailEqualSpecification(accountEmail));
            return GenerateTokenCore(account);
        }
    }
}