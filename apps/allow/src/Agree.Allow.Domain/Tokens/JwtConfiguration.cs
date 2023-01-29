namespace Agree.Allow.Domain.Tokens;

public class JwtConfiguration
{
    public string SigningKey { get; set; }
    public string Issuer { get; set; }
    public int AccessTokenExpiresInMinutes { get; set; }
    public int RefreshTokenExpiresInDays { get; set; }
}