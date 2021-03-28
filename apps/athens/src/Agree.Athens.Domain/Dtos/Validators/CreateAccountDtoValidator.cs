using System.Text.RegularExpressions;
using FluentValidation;

namespace Agree.Athens.Domain.Dtos.Validators
{
    public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountDtoValidator()
        {
            RuleFor(dto => dto.Password)
                .MinimumLength(7).WithMessage("Password must have at least 7 characters")
                .MaximumLength(40).WithMessage("Password must not have more than 40 characters")
                .Must(HaveValidPassword).WithMessage("Password must contain at least one digit, one lowercase letter and one uppercase letter");

            RuleFor(dto => dto.UserName)
                .MinimumLength(1).WithMessage("UserName must have at least 1 character")
                .MaximumLength(20).WithMessage("UserName must not have more than 20 characters");

            RuleFor(dto => dto.Email)
                .EmailAddress().WithMessage("Email must be a valid email address")
                .MaximumLength(255).WithMessage("Email must not have more than 255 characters");
        }

        private bool HaveValidPassword(string password)
        {
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("(\\d)+");

            return (lowercase.IsMatch(password) && uppercase.IsMatch(password) && digit.IsMatch(password));
        }
    }
}