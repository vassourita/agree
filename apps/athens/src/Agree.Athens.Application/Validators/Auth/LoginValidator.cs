using Agree.Athens.Application.Dtos.Auth;
using FluentValidation;

namespace Agree.Athens.Application.Validators.Auth
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(createAccountDto => createAccountDto.Email)
                .NotNull().WithMessage("Email is required")
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Email must be a valid email account");
            RuleFor(createAccountDto => createAccountDto.Password)
                .NotNull().WithMessage("Password is required")
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(7).WithMessage("Password must have at least 7 character")
                .Matches("[0-9]").WithMessage("Password must have at least 1 number");
        }
    }
}