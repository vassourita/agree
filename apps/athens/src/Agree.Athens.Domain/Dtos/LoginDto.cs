using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Dtos
{
    public class LoginDto : Validatable
    {
        public string GrantType { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public string IpAddress { get; set; }
    }
}