namespace Agree.Allow.Domain.Test.Tokens;

using Agree.Allow.Domain.Tokens;
using Microsoft.Extensions.Options;

public class AccessTokenFactoryTest : TokenTest
{
    private readonly AccessTokenFactory _sut;
    private readonly JwtConfiguration _jwtConfiguration;

    public AccessTokenFactoryTest()
    {
        _sut = Resolve<AccessTokenFactory>();
        _jwtConfiguration = Resolve<IOptions<JwtConfiguration>>().Value;
    }

    [Fact]
    public async Task GenerateAsync_ShouldCreateAccessToken()
    {
        var userAccount = await CreateTestUserAccount();
        var token = await _sut.GenerateAsync(userAccount);
        Assert.NotNull(token);
        Assert.NotNull(token.Type);
        Assert.NotNull(token.TokenValue);
        Assert.True(token.ExpiresIn != default(long));
    }

    [Fact]
    public async Task GenerateAsync_ShouldGenerateAsyncAccessTokenWithCorrectType()
    {
        var userAccount = await CreateTestUserAccount();
        var token = await _sut.GenerateAsync(userAccount);
        Assert.Equal("Bearer", token.Type);
    }

    [Fact]
    public async Task GenerateAsync_ShouldCreateAccessTokenWithValidExpirationDate()
    {
        var userAccount = await CreateTestUserAccount();
        var expectedExpirationDate = DateTime.UtcNow.AddMinutes(_jwtConfiguration.AccessTokenExpiresInMinutes);
        var expected = ((DateTimeOffset)expectedExpirationDate).ToUnixTimeSeconds();
        var token = await _sut.GenerateAsync(userAccount);
        Assert.True(token.ExpiresIn <= expected);
        Assert.True(token.ExpiresIn > expected - 1);
    }

    [Fact]
    public async Task GenerateAsync_ShouldCreateAccessTokenWithExpectedClaims()
    {
        var userAccount = await CreateTestUserAccount();
        var token = await _sut.GenerateAsync(userAccount);
        var decoded = DecodeToken(token.TokenValue);
        Assert.Equal(userAccount.Id.ToString(), decoded.Claims.Single(x => x.Type == "sub").Value);
        Assert.Equal(userAccount.EmailAddress, decoded.Claims.Single(x => x.Type == "email").Value);
        Assert.Equal(userAccount.NameTag, decoded.Claims.Single(x => x.Type == "name").Value);
    }

    [Fact]
    public async Task GenerateAsync_ShouldCreateAccessTokenWithExpectedIssuer()
    {
        var userAccount = await CreateTestUserAccount();
        var token = await _sut.GenerateAsync(userAccount);
        var decoded = DecodeToken(token.TokenValue);
        Assert.Equal(_jwtConfiguration.Issuer, decoded.Issuer);
    }
}