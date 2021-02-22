using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Account.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(account => account.UserName)
                .MinimumLength(1).WithMessage("UserName must have at least 1 character")
                .MaximumLength(20).WithMessage("UserName must not have more than 20 characters");

            RuleFor(account => account.Email)
                .EmailAddress().WithMessage("Email must be a valid email address")
                .MaximumLength(255).WithMessage("Email must not have more than 255 characters");

            RuleFor(account => account.PasswordHash)
                .MinimumLength(1).WithMessage("Password must have at least one character")
                .MaximumLength(400).WithMessage("Password too big");
        }
    }
}