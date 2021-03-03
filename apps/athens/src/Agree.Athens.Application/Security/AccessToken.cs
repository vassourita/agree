namespace Agree.Athens.Application.Security
{
    public class AccessToken
    {
        public string Token { get; set; }
        public long ExpiresIn { get; set; }
        public string Type { get; set; }
    }
}