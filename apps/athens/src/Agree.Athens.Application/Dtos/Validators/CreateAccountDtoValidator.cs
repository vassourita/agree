using System.Text.RegularExpressions;
using FluentValidation;

namespace Agree.Athens.Application.Dtos.Validators
{
    public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountDtoValidator()
        {
            RuleFor(dto => dto.Password)
                .MinimumLength(7).WithMessage("Password must have at least 7 characters")
                .MaximumLength(40).WithMessage("Password must not have more than 40 characters")
                .Must(HasValidPassword).WithMessage("Password must contain at least one digit, one lowercase letter and one uppercase letter");
        }

        private bool HasValidPassword(string password)
        {
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("(\\d)+");

            return (lowercase.IsMatch(password) && uppercase.IsMatch(password) && digit.IsMatch(password));
        }
    }
}