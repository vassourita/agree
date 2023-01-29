namespace Agree.Allow.Domain.Test.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Agree.Allow.Domain.Tokens;
using Microsoft.Extensions.Options;

public class TokenValidatorTest : TokenTest
{
    private readonly JwtConfiguration _jwtConfiguration;

    public TokenValidatorTest()
    {
        _jwtConfiguration = Resolve<IOptions<JwtConfiguration>>().Value;
    }

    [Fact]
    public async Task ValidateAsync_WithValidAccessToken_ReturnsUserAccount()
    {
        // Arrange
        var (expectedAccount, token) = await GenerateAccessToken();
        var sut = Resolve<TokenValidator>();

        // Act
        var account = await sut.ValidateAsync(token);

        // Assert
        Assert.NotNull(account);
        Assert.Equal(expectedAccount.Id, account.Id);
    }

    [Fact]
    public async Task ValidateAsync_WithInvalidToken_ReturnsNull()
    {
        // Arrange
        var (_, token) = await GenerateAccessToken();
        var sut = Resolve<TokenValidator>();

        // Act
        var account = await sut.ValidateAsync(token + "invalid");

        // Assert
        Assert.Null(account);
    }

    [Fact]
    public async Task ValidateAsync_WithValidRefreshToken_ReturnsUserAccount()
    {
        // Arrange
        var (expectedAccount, token) = await GenerateRefreshToken();
        var sut = Resolve<TokenValidator>();

        // Act
        var account = await sut.ValidateAsync(token, true);

        // Assert
        Assert.NotNull(account);
        Assert.Equal(expectedAccount.Id, account.Id);
    }

    [Fact]
    public async Task ValidateAsync_WithInvalidRefreshToken_ReturnsNull()
    {
        // Arrange
        var (_, token) = await GenerateRefreshToken();
        var sut = Resolve<TokenValidator>();

        // Act
        var account = await sut.ValidateAsync(token + "invalid", true);

        // Assert
        Assert.Null(account);
    }

    [Fact]
    public async Task ValidateAsync_WithValidateRefreshFlagAndAccessToken_ReturnsNull()
    {
        // Arrange
        var (_, token) = await GenerateAccessToken();
        var sut = Resolve<TokenValidator>();

        // Act
        var account = await sut.ValidateAsync(token, true);

        // Assert
        Assert.Null(account);
    }
}