using System.Linq;
using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Account.Validators
{
    public class UserTagValidator : AbstractValidator<UserTag>
    {
        public UserTagValidator()
        {
            RuleFor(tag => tag.Value)
                .Length(4).WithMessage("UserTag must have 4 numbers")
                .Must(BeNumeric).WithMessage("UserTag must be numeric")
                .Must(NotBeAllZeros).WithMessage("UserTag must not be all zeros");
        }

        private bool BeNumeric(string tag) => int.TryParse(tag, out _);

        private bool NotBeAllZeros(string tag) => !(tag.All(c => c == '0'));
    }
}