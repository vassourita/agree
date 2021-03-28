using System;
using System.Linq;
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

            RuleFor(dto => dto.Privacy)
                .Must(BeValidPrivacy).WithMessage("Server privacy must be one of 'Private', 'Public' or 'Open'");
        }

        private bool BeValidPrivacy(string arg)
        {
            var options = new[] { "Private", "Public", "Open" };
            return options.Any(opt => opt == arg);
        }
    }
}