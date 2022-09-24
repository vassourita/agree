namespace Agree.Accord.Domain.Identity.Tokens;

/// <summary>
/// The representation of an access token.
/// </summary>
public class AccessToken
{
    /// <summary>
    /// The token value.
    /// </summary>
    /// <value>The token itself.</value>
    public string Token { get; set; }

    /// <summary>
    /// The token expiration time.
    /// </summary>
    /// <value>The expiration time in UnixTime seconds.</value>
    public long ExpiresIn { get; set; }

    /// <summary>
    /// The token type.
    /// </summary>
    /// <value>The token type.</value>
    public string Type { get; set; }
}