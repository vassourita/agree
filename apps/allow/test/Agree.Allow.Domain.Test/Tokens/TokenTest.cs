namespace Agree.Allow.Domain.Test.Tokens;

using System.IdentityModel.Tokens.Jwt;
using Agree.Allow.Domain.Tokens;
using Agree.Allow.Test;

public class TokenTest : TestBase
{
    protected JwtSecurityToken DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var decoded = handler.ReadJwtToken(token);
        return decoded;
    }

    protected async Task<(UserAccount, string)> GenerateAccessToken()
    {
        var accessTokenFactory = Resolve<AccessTokenFactory>();
        var account = await CreateTestUserAccount();
        var token = await accessTokenFactory.GenerateAsync(account);
        return (account, token.TokenValue);
    }

    protected async Task<(UserAccount, string)> GenerateRefreshToken()
    {
        var refreshTokenFactory = Resolve<RefreshTokenFactory>();
        var account = await CreateTestUserAccount();
        var token = await refreshTokenFactory.GenerateAsync(account);
        return (account, token.TokenValue);
    }
}