namespace Agree.Allow.Domain.Tokens;

using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class RefreshTokenFactory
{
    private readonly JwtConfiguration _jwtConfiguration;

    public RefreshTokenFactory(IOptions<JwtConfiguration> jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration.Value;
    }

    private Token GenerateRefreshTokenCore(UserAccount account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.SigningKey);

        var expiresIn = DateTime.UtcNow.AddDays(_jwtConfiguration.RefreshTokenExpiresInDays);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim("refresh", "y")
            }),
            Issuer = _jwtConfiguration.Issuer,
            Expires = expiresIn,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return new Token(tokenHandler.WriteToken(token), ((DateTimeOffset)expiresIn).ToUnixTimeSeconds(), "Bearer");
    }

    public Task<Token> GenerateAsync(UserAccount account) => Task.Run(() => GenerateRefreshTokenCore(account));
}