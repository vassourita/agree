namespace Agree.Accord.Domain.Identity.Tokens
{
    public class JwtConfiguration
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}