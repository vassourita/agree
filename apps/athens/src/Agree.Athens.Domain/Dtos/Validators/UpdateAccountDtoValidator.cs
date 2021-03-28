using FluentValidation;

namespace Agree.Athens.Domain.Dtos.Validators
{
    public class UpdateAccountDtoValidator : AbstractValidator<UpdateAccountDto>
    {
        public UpdateAccountDtoValidator()
        {
            RuleFor(dto => dto.PasswordConfirmation)
                .NotEmpty().WithMessage("Password confirmation must not be empty");
        }
    }
}