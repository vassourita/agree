using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Servers.Validators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(role => role.Name)
                .MinimumLength(1).WithMessage(r => $"{r} name must have at least 1 character")
                .MaximumLength(20).WithMessage(r => $"{r} name must not have more than 20 characters");
        }
    }
}