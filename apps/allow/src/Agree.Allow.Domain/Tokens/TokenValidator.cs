namespace Agree.Allow.Domain.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Agree.Allow.Domain.Specifications;
using Agree.SharedKernel.Data;
using Microsoft.IdentityModel.Tokens;

public class TokenValidator
{
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly IRepository<UserAccount, Guid> _accountRepository;

    public TokenValidator(JwtConfiguration jwtConfiguration,
                    IRepository<UserAccount, Guid> accountRepository)
    {
        _jwtConfiguration = jwtConfiguration;
        _accountRepository = accountRepository;
    }

    public async Task<UserAccount> ValidateAsync(string token, bool validateRefresh = false)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.SigningKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _jwtConfiguration.Issuer,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

        var accountId = Guid.Parse(principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

        if (validateRefresh)
        {
            var refreshTokenCheck = principal.Claims.First(x => x.Type == "refresh_token").Value;
            if (string.IsNullOrWhiteSpace(refreshTokenCheck) || refreshTokenCheck != "y")
                return null;
        }

        var account = await _accountRepository.GetFirstAsync(new UserIdEqualSpecification(accountId));

        return account;
    }
}