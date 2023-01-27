namespace Agree.Allow.Domain.Tokens;

/// <summary>
/// The JWT access token configuration.
/// </summary>
public class JwtConfiguration
{
    public string SigningKey { get; set; }
    public string Issuer { get; set; }
    public string[] Audiences { get; set; }
    public int AccessTokenExpiresInMinutes { get; set; }
    public int RefreshTokenExpiresInDays { get; set; }
}