namespace Agree.Accord.Domain.Identity.Tokens
{
    /// <summary>
    /// The JWT access token configuration.
    /// </summary>
    public class JwtConfiguration
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}