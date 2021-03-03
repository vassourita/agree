using Agree.Athens.Application.Security;

namespace Agree.Athens.Application.Dtos
{
    public class LoginDto
    {
        public string GrantType { get; set; } = GrantTypes.Password;
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public string IpAddress { get; set; }
    }
}