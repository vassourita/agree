using Agree.Athens.Application.Security;

namespace Agree.Athens.Presentation.WebApi.Models
{
    public class LoginResponse : Response
    {
        public LoginResponse(AccessToken accessToken, RefreshToken refreshToken)
            : base("")
        {
            IsAuthenticated = accessToken.Token != null;
            AccessToken = accessToken.Token;
            ExpiresIn = accessToken.ExpiresIn;
            RefreshToken = refreshToken.Token;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long? ExpiresIn { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}