namespace Agree.Allow.Domain.Tokens;

/// <summary>
/// The representation of an token.
/// </summary>
public class Token
{
    public Token(string tokenValue, long expiresIn, string type)
    {
        TokenValue = tokenValue;
        ExpiresIn = expiresIn;
        Type = type;
    }

    /// <summary>
    /// The token value.
    /// </summary>
    /// <value>The token itself.</value>
    public string TokenValue { get; private set; }

    /// <summary>
    /// The token expiration time.
    /// </summary>
    /// <value>The expiration time in UnixTime seconds.</value>
    public long ExpiresIn { get; private set; }

    /// <summary>
    /// The token type.
    /// </summary>
    /// <value>The token type.</value>
    public string Type { get; private set; }
}

public record TokenCollection(Token AccessToken, Token RefreshToken);