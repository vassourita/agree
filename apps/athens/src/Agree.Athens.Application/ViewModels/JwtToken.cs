namespace Agree.Athens.Application.ViewModels
{
    public class JwtToken
    {
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
    }
}