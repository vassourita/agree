using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Agree.Athens.Infrastructure.Configuration;
using Agree.Athens.Domain.Aggregates.Account;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Agree.Athens.Application.Services
{
    public class TokenService
    {
        public readonly JwtConfiguration _jwtConfiguration;

        public TokenService(IOptions<JwtConfiguration> jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration.Value;
        }

        public (string, long) GenerateAccessToken(UserAccount account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.UserName),
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim("Id", account.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _jwtConfiguration.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var expiresIn = token.ValidTo.Subtract(DateTime.UtcNow).Ticks;
            return (tokenHandler.WriteToken(token), expiresIn);
        }
    }
}