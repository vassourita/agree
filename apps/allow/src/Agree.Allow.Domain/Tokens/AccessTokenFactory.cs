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
    private readonly IRepository<UserAccount, Guid> _accountRepository;
    private readonly RefreshTokenFactory _refreshTokenFactory;
    private readonly TokenValidator _tokenValidator;
    private readonly string _currentAudience;

    public AccessTokenFactory(IOptions<JwtConfiguration> jwtConfiguration,
                        IRepository<UserAccount, Guid> accountRepository,
                        RefreshTokenFactory refreshTokenFactory,
                        TokenValidator tokenValidator,
                        string currentAudience)
    {
        _jwtConfiguration = jwtConfiguration.Value;
        _accountRepository = accountRepository;
        _refreshTokenFactory = refreshTokenFactory;
        _tokenValidator = tokenValidator;
        _currentAudience = currentAudience;
    }

    private Token GenerateAccessTokenCore(UserAccount account)
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
            Audience = _currentAudience,
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
    public Task<Token> GenerateAsync(UserAccount account) => Task.Run(() => GenerateAccessTokenCore(account));
}