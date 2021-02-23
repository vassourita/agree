using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Account.Validators
{
    public class UserAccountValidator : AbstractValidator<UserAccount>
    {
        public UserAccountValidator()
        {
            RuleFor(account => account.UserName)
                .MinimumLength(1).WithMessage(a => "{a} userName must have at least 1 character")
                .MaximumLength(20).WithMessage(a => "{a} userName must not have more than 20 characters");

            RuleFor(account => account.Email)
                .EmailAddress().WithMessage(a => "{a} email must be a valid email address")
                .MaximumLength(255).WithMessage(a => "{a} email must not have more than 255 characters");

            RuleFor(account => account.PasswordHash)
                .MinimumLength(1).WithMessage(a => "{a} password must have at least one character")
                .MaximumLength(400).WithMessage(a => "{a} password too big");
        }
    }
}