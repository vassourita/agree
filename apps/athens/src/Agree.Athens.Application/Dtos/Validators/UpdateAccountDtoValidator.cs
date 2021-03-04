using System;
using System.Linq;
using FluentValidation;

namespace Agree.Athens.Application.Dtos.Validators
{
    public class UpdateAccountDtoValidator : AbstractValidator<UpdateAccountDto>
    {
        public UpdateAccountDtoValidator()
        {
            RuleFor(account => account.PasswordConfirmation)
                .NotNull().WithMessage("Password must not be null")
                .NotEmpty().WithMessage("Password must not be empty");
        }
    }
}