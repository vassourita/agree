using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Agree.Allow.Domain.Security;
using Agree.Allow.Domain.Services;
using Agree.Allow.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Agree.Allow.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly TokenConfiguration _tokenConfiguration;

        public TokenService(
            UserManager<UserAccount> userManager,
            IOptions<TokenConfiguration> tokenConfiguration)
        {
            _userManager = userManager;
            _tokenConfiguration = tokenConfiguration.Value;
        }

        public async Task<string> GenerateToken(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Name, $"{user.DisplayName}#{user.Tag.ToString().PadLeft(4, '0')}"),
                     new Claim(ClaimTypes.Email, user.Email),
                     new Claim(ClaimTypes.Role, "user"),
                     new Claim("verified", user.EmailConfirmed.ToString().ToLower()),
                     new Claim("id", user.Id.ToString()),
                }),
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                Expires = DateTime.UtcNow.AddHours(_tokenConfiguration.ExpiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}