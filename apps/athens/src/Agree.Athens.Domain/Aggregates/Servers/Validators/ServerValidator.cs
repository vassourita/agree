using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Servers.Validators
{
    public class ServerValidator : AbstractValidator<Server>
    {
        public ServerValidator()
        {
            RuleFor(server => server.Name)
                .MinimumLength(1).WithMessage("Server name must have at least 1 character")
                .MaximumLength(30).WithMessage("Server name must not have more than 30 characters");
        }
    }
}