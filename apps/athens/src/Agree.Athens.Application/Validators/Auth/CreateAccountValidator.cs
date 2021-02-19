using Agree.Athens.Application.Dtos.Auth;
using FluentValidation;

namespace Agree.Athens.Application.Validators.Auth
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidator()
        {
            RuleFor(createAccountDto => createAccountDto.Username)
                .NotNull().WithMessage("Username is required")
                .NotEmpty().WithMessage("Username cannot be empty")
                .MinimumLength(1).WithMessage("Username must have at least 1 character")
                .MaximumLength(20).WithMessage("Username cannot have more than 20 characters");
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