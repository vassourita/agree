using Agree.Athens.SharedKernel;
using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Servers.Validators
{
    public class TextChannelValidator : AbstractValidator<TextChannel>
    {
        public TextChannelValidator()
        {
            RuleFor(channel => channel.Name)
                .MinimumLength(1).WithMessage("Channel name must have at least 1 character")
                .MaximumLength(20).WithMessage("Channel name must not have more than 20 characters");
        }
    }
}