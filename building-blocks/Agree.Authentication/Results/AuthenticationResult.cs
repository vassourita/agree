namespace Agree.Authentication.Results;

public record Token(string TokenValue, string TokenType, int ExpiresIn);

public record AuthenticationResult(Token AccessToken, Token RefreshToken);