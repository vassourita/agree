namespace Agree.Accord.Domain.Identity.Tokens
{
    public class AccessToken
    {
        public string Token { get; set; }
        public long ExpiresIn { get; set; }
        public string Type { get; set; }
    }
}