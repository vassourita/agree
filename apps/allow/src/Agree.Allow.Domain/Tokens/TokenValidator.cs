namespace Agree.Allow.Domain.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Agree.Allow.Domain.Specifications;
using Agree.SharedKernel.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class TokenValidator
{
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly IRepository<UserAccount, Guid> _accountRepository;

    public TokenValidator(IOptions<JwtConfiguration> jwtConfiguration,
                    IRepository<UserAccount, Guid> accountRepository)
    {
        _jwtConfiguration = jwtConfiguration.Value;
        _accountRepository = accountRepository;
    }

    public async Task<UserAccount> ValidateAsync(string token, bool validateRefresh = false)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.MapInboundClaims = false;
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.SigningKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _jwtConfiguration.Issuer,
            ValidateLifetime = true,
            ValidateAudience = false
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            var accountId = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrWhiteSpace(accountId))
                return null;

            if (validateRefresh)
            {
                var refreshTokenCheck = principal.Claims.FirstOrDefault(x => x.Type == "rsh")?.Value;
                if (string.IsNullOrWhiteSpace(refreshTokenCheck) || refreshTokenCheck != "y")
                    return null;
            }

            var account = await _accountRepository.GetFirstAsync(new UserIdEqualSpecification(Guid.Parse(accountId)));

            return account;
        }
        catch (SecurityTokenException)
        {
            return null;
        }
    }
}