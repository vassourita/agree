using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Agree.Athens.Infrastructure.Configuration;
using Agree.Athens.Domain.Aggregates.Account;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Agree.Athens.Application.ViewModels;

namespace Agree.Athens.Application.Services
{
    public class TokenService
    {
        public readonly JwtConfiguration _jwtConfiguration;

        public TokenService(IOptions<JwtConfiguration> jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration.Value;
        }

        public JwtToken GenerateAccessToken(UserAccount account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key);

            var expiresIn = DateTime.UtcNow.AddHours(2);
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
            return new JwtToken
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = expiresIn.Subtract(DateTime.UtcNow).Ticks
            };
        }
    }
}