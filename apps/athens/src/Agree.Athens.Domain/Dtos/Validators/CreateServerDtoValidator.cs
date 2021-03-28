using FluentValidation;

namespace Agree.Athens.Domain.Dtos.Validators
{
    public class CreateServerDtoValidator : AbstractValidator<CreateServerDto>
    {
        public CreateServerDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .MinimumLength(1).WithMessage("Server name must have at least 1 character")
                .MaximumLength(30).WithMessage("Server name must not have more than 30 characters");

            RuleFor(dto => dto.Description)
                .MaximumLength(300).WithMessage("Server description must not have more than 300 characters");
        }
    }
}