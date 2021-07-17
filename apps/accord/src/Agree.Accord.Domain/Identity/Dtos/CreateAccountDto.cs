using System.ComponentModel.DataAnnotations;

namespace Agree.Accord.Domain.Identity.Dtos
{
    public class CreateAccountDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [MinLength(1)]
        [MaxLength(40)]
        [RegularExpression(UserNameRegEx, ErrorMessage = "UserName must only contain alfabetic characters, digits, _ and -.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6)]
        [MaxLength(100)]
        [RegularExpression(PasswordRegEx, ErrorMessage = "Password must contain at least one uppercase character, one lowercase character, and a digit.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required.")]
        [Compare("Password", ErrorMessage = "Passwords doesn't match.")]
        public string PasswordConfirmation { get; set; }

        private const string UserNameRegEx = @"/^[a-zA-Z0-9_-]*$/g";
        private const string PasswordRegEx = @"/(^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$)?(^(?=.*\d)(?=.*[a-z])(?=.*[@#$%^&+=]).*$)?(^(?=.*\d)(?=.*[A-Z])(?=.*[@#$%^&+=]).*$)?(^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$)?/g";
    }
}