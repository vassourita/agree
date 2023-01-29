namespace Agree.Allow.Domain.Test.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Agree.Allow.Domain.Tokens;
using Microsoft.Extensions.Options;

public class TokenValidatorTest : TokenTest
{
    private readonly TokenValidator _sut;
    private readonly JwtConfiguration _jwtConfiguration;

    public TokenValidatorTest()
    {
        _sut = Resolve<TokenValidator>();
        _jwtConfiguration = Resolve<IOptions<JwtConfiguration>>().Value;
    }

    [Fact]
    public async Task ValidateAsync_WithValidAccessToken_ReturnsUserAccount()
    {
        var (expectedAccount, token) = await GenerateAccessToken();
        var account = await _sut.ValidateAsync(token);

        Assert.NotNull(account);
        Assert.Equal(expectedAccount.Id, account.Id);
    }

    [Fact]
    public async Task ValidateAsync_WithInvalidToken_ReturnsNull()
    {
        var (_, token) = await GenerateAccessToken();
        var account = await _sut.ValidateAsync(token + "invalid");

        Assert.Null(account);
    }

    [Fact]
    public async Task ValidateAsync_WithValidRefreshToken_ReturnsUserAccount()
    {
        var (expectedAccount, token) = await GenerateRefreshToken();
        var account = await _sut.ValidateAsync(token, true);

        Assert.NotNull(account);
        Assert.Equal(expectedAccount.Id, account.Id);
    }

    [Fact]
    public async Task ValidateAsync_WithInvalidRefreshToken_ReturnsNull()
    {
        var (_, token) = await GenerateRefreshToken();
        var account = await _sut.ValidateAsync(token + "invalid", true);

        Assert.Null(account);
    }

    [Fact]
    public async Task ValidateAsync_WithValidateRefreshFlagAndAccessToken_ReturnsNull()
    {
        var (_, token) = await GenerateAccessToken();
        var account = await _sut.ValidateAsync(token, true);

        Assert.Null(account);
    }
}