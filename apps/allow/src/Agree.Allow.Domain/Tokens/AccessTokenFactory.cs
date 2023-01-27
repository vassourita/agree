namespace Agree.Allow.Domain.Tokens;

using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Agree.Allow.Domain.Specifications;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Agree.SharedKernel.Data;

/// <summary>
/// Service for creating and managing access tokens.
/// </summary>
public class AccessTokenFactory
{
    private readonly JwtConfiguration _jwtConfiguration;

    public AccessTokenFactory(IOptions<JwtConfiguration> jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration.Value;
    }

    private Token GenerateAccessTokenCore(UserAccount account, string audience)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.SigningKey);

        var expiresIn = DateTime.UtcNow.AddMinutes(_jwtConfiguration.AccessTokenExpiresInMinutes);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, account.NameTag),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.EmailAddress)
            }),
            Issuer = _jwtConfiguration.Issuer,
            Expires = expiresIn,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return new Token(tokenHandler.WriteToken(token), ((DateTimeOffset)expiresIn).ToUnixTimeSeconds(), "Bearer");
    }

    /// <summary>
    /// Generates a access token for a given user.
    /// </summary>
    /// <param name="account">The user account.</param>
    /// <returns>The generated access token.</returns>
    public Task<Token> GenerateAsync(UserAccount account, string audience) => Task.Run(() => GenerateAccessTokenCore(account, audience));
}