using System;
using FluentValidation;

namespace Agree.Athens.Application.Dtos.Validators
{
    public class UpdateAccountDtoValidator : AbstractValidator<UpdateAccountDto>
    {
        public UpdateAccountDtoValidator()
        {
            RuleFor(account => account.UserName)
                .MinimumLength(1).WithMessage("UserName must have at least 1 character")
                .MaximumLength(20).WithMessage("UserName must not have more than 20 characters");

            RuleFor(account => account.Email)
                .EmailAddress().WithMessage("Email must be a valid email address")
                .MaximumLength(255).WithMessage("Email must not have more than 255 characters");

            RuleFor(account => account.UserId)
                .NotNull().WithMessage("UserId must not be null")
                .Must(id => id != Guid.Empty).WithMessage("UserId must not be an empty Guid");
        }
    }
}