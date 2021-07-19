using System.ComponentModel.DataAnnotations;

namespace Agree.Accord.Domain.Identity.Dtos
{
    public class CreateAccountDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        [MaxLength(255, ErrorMessage = "Email must have less than 255 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [MinLength(1, ErrorMessage = "UserName must have at least 1 character.")]
        [MaxLength(40, ErrorMessage = "UserName must have less than 40 characters.")]
        [RegularExpression(UserNameRegEx, ErrorMessage = "UserName must only contain alfabetic characters, digits, _ and -.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must have at least 6 characters.")]
        [MaxLength(80, ErrorMessage = "Password must have less than 80 characters.")]
        [RegularExpression(PasswordRegEx, ErrorMessage = "Password must contain at least one uppercase character, one lowercase character, and a digit.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required.")]
        [Compare("Password", ErrorMessage = "Passwords doesn't match.")]
        public string PasswordConfirmation { get; set; }

        private const string UserNameRegEx = @"[a-zA-Z0-9_-]*";
        private const string PasswordRegEx = @"(^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$)?(^(?=.*\d)(?=.*[a-z])(?=.*[@#$%^&+=]).*$)?(^(?=.*\d)(?=.*[A-Z])(?=.*[@#$%^&+=]).*$)?(^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$)?";
    }
}