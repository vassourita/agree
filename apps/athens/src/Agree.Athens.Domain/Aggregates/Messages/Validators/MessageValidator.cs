using System.Data;
using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Messages.Validators
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(message => message.Content)
                .MinimumLength(1).WithMessage(m => "{m} content must have at least 1 character")
                .MaximumLength(800).WithMessage(m => "{m} content must not have more than 800 characters");
        }
    }
}