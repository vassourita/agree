namespace Agree.Allow.Domain.Test.Tokens;

using System.IdentityModel.Tokens.Jwt;
using Agree.Allow.Domain.Tokens;
using Microsoft.Extensions.Options;

public class RefreshTokenFactoryTest : TokenTest
{
    private readonly RefreshTokenFactory _sut;
    private readonly JwtConfiguration _jwtConfiguration;

    public RefreshTokenFactoryTest()
    {
        _sut = Resolve<RefreshTokenFactory>();
        _jwtConfiguration = Resolve<IOptions<JwtConfiguration>>().Value;
    }

    [Fact]
    public async Task GenerateAsync_ShouldCreateRefreshToken()
    {
        var userAccount = await CreateTestUserAccount();
        var token = await _sut.GenerateAsync(userAccount);
        Assert.NotNull(token);
        Assert.NotNull(token.Type);
        Assert.NotNull(token.TokenValue);
        Assert.True(token.ExpiresIn != default(long));
    }

    [Fact]
    public async Task GenerateAsync_ShouldGenerateAsyncRefreshTokenWithCorrectType()
    {
        var userAccount = await CreateTestUserAccount();
        var token = await _sut.GenerateAsync(userAccount);
        Assert.Equal("Bearer", token.Type);
    }

    [Fact]
    public async Task GenerateAsync_ShouldCreateRefreshTokenWithValidExpirationDate()
    {
        var userAccount = await CreateTestUserAccount();
        var expectedExpirationDate = DateTime.UtcNow.AddDays(_jwtConfiguration.RefreshTokenExpiresInDays);
        var expected = ((DateTimeOffset)expectedExpirationDate).ToUnixTimeSeconds();
        var token = await _sut.GenerateAsync(userAccount);
        Assert.True(token.ExpiresIn <= expected);
        Assert.True(token.ExpiresIn > expected - 1);
    }

    [Fact]
    public async Task GenerateAsync_ShouldCreateRefreshTokenWithExpectedClaims()
    {
        var userAccount = await CreateTestUserAccount();
        var token = await _sut.GenerateAsync(userAccount);
        var decoded = DecodeToken(token.TokenValue);
        Assert.Equal(userAccount.Id.ToString(), decoded.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
        Assert.Equal("y", decoded.Claims.Single(x => x.Type == "rsh").Value);
    }

    [Fact]
    public async Task GenerateAsync_ShouldCreateRefreshTokenWithExpectedIssuer()
    {
        var userAccount = await CreateTestUserAccount();
        var token = await _sut.GenerateAsync(userAccount);
        var decoded = DecodeToken(token.TokenValue);
        Assert.Equal(_jwtConfiguration.Issuer, decoded.Issuer);
    }
}