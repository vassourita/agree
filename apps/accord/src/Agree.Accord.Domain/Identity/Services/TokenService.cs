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

namespace Agree.Accord.Domain.Identity.Services
{
    public class TokenService
    {
        public readonly JwtConfiguration _jwtConfiguration;
        public readonly IRepository<UserAccount> _accountRepository;

        public TokenService(IOptions<JwtConfiguration> jwtConfiguration,
                            IRepository<UserAccount> accountRepository)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _accountRepository = accountRepository;
        }

        public Task<AccessToken> GenerateAccessTokenAsync(UserAccount account)
        {
            return Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfiguration.SigningKey);

                var expiresIn = DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpiresInMinutes);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, account.NameTag),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.Role, "user"),
                        new Claim("id", account.Id.ToString()),
                    }),
                    Expires = expiresIn,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
                    Issuer = _jwtConfiguration.Issuer,
                };

                var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                return new AccessToken
                {
                    Token = tokenHandler.WriteToken(token),
                    ExpiresIn = ((DateTimeOffset)expiresIn).ToUnixTimeSeconds(),
                    Type = "Bearer"
                };
            });
        }
    }
}