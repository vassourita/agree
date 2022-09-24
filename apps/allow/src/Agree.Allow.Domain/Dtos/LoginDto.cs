using System.ComponentModel.DataAnnotations;

namespace Agree.Allow.Domain.Dtos
{
    public class LoginCommand
    {
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} must be a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }
    }
}