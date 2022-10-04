namespace Agree.Accord.Domain.Identity.Tokens;

using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Agree.Accord.Domain.Identity.Specifications;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Agree.Accord.SharedKernel.Data;

/// <summary>
/// Service for creating and managing tokens.
/// </summary>
public class TokenFactory
{
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly IRepository<UserAccount, Guid> _accountRepository;

    public TokenFactory(IOptions<JwtConfiguration> jwtConfiguration,
                        IRepository<UserAccount, Guid> accountRepository)
    {
        _jwtConfiguration = jwtConfiguration.Value;
        _accountRepository = accountRepository;
    }

    private AccessToken GenerateTokenCore(UserAccount account)
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
                new Claim(ClaimTypes.Email, account.EmailAddress),
                new Claim(ClaimTypes.Role, "user")
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

    /// <summary>
    /// Generates a access token for a given user.
    /// </summary>
    /// <param name="account">The user account.</param>
    /// <returns>The generated access token.</returns>
    public Task<AccessToken> GenerateAccessTokenAsync(UserAccount account) => Task.Run(() => GenerateTokenCore(account));

    /// <summary>
    /// Generates a access token for a given user.
    /// </summary>
    /// <param name="accountEmail">The user email.</param>
    /// <returns>The generated access token.</returns>
    public async Task<AccessToken> GenerateAccessTokenAsync(string accountEmail)
    {
        var account = await _accountRepository.GetFirstAsync(new EmailEqualSpecification(accountEmail));
        return GenerateTokenCore(account);
    }
}