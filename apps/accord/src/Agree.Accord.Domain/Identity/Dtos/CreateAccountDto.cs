namespace Agree.Accord.Domain.Identity.Dtos;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// The <see cref="CreateAccountDto"/> class represents a request to create an account.
/// </summary>
public class CreateAccountDto
{
    /// <summary>
    /// The email address of the account.
    /// </summary>
    /// <value>The email address.</value>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
    [MaxLength(255, ErrorMessage = "Email must have less than 255 characters.")]
    public string Email { get; set; }

    /// <summary>
    /// The username of the account.
    /// </summary>
    /// <value>The username.</value>
    [Required(ErrorMessage = "Username is required.")]
    [MinLength(1, ErrorMessage = "Username must have at least 1 character.")]
    [MaxLength(40, ErrorMessage = "Username must have less than 40 characters.")]
    [RegularExpression(UsernameRegEx, ErrorMessage = "Username must only contain alfabetic characters, digits, _ and -.")]
    public string Username { get; set; }

    /// <summary>
    /// The password of the account.
    /// </summary>
    /// <value>The password.</value>
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must have at least 6 characters.")]
    [MaxLength(80, ErrorMessage = "Password must have less than 80 characters.")]
    [RegularExpression(PasswordRegEx, ErrorMessage = "Password must contain at least one uppercase character, one lowercase character, and a digit.")]
    public string Password { get; set; }

    /// <summary>
    /// The register password confirmation.
    /// </summary>
    /// <value>The password confirmation.</value>
    [Required(ErrorMessage = "Password confirmation is required.")]
    [Compare("Password", ErrorMessage = "Passwords doesn't match.")]
    public string PasswordConfirmation { get; set; }

    /// <summary>
    /// A <see cref="System.String"/> that represents the regular expression used to validate the Username.
    /// </summary>
    private const string UsernameRegEx = @"[a-zA-Z0-9_-]*";

    /// <summary>
    /// A <see cref="System.String"/> that represents the regular expression used to validate the Password.
    /// </summary>
    private const string PasswordRegEx = @"(^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$)?(^(?=.*\d)(?=.*[a-z])(?=.*[@#$%^&+=]).*$)?(^(?=.*\d)(?=.*[A-Z])(?=.*[@#$%^&+=]).*$)?(^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$)?";
}