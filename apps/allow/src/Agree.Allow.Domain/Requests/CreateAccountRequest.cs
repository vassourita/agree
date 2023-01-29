namespace Agree.Allow.Domain.Requests;

using System.ComponentModel.DataAnnotations;
using Agree.Allow.Domain.Results;
using MediatR;

public class CreateAccountRequest : IRequest<CreateAccountResult>
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
    [MaxLength(255, ErrorMessage = "Email must have less than 255 characters.")]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [MinLength(1, ErrorMessage = "Username must have at least 1 character.")]
    [MaxLength(40, ErrorMessage = "Username must have less than 40 characters.")]
    [RegularExpression(UsernameRegEx, ErrorMessage = "Username must only contain alfabetic characters, digits, _ and -.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must have at least 6 characters.")]
    [MaxLength(80, ErrorMessage = "Password must have less than 80 characters.")]
    [RegularExpression(PasswordRegEx, ErrorMessage = "Password must contain at least one uppercase character, one lowercase character, and a digit.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Password confirmation is required.")]
    [Compare("Password", ErrorMessage = "Passwords doesn't match.")]
    public string PasswordConfirmation { get; set; }

    private const string UsernameRegEx = @"[a-zA-Z0-9_-]*";

    private const string PasswordRegEx = @"(^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$)?(^(?=.*\d)(?=.*[a-z])(?=.*[@#$%^&+=]).*$)?(^(?=.*\d)(?=.*[A-Z])(?=.*[@#$%^&+=]).*$)?(^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$)?";
}