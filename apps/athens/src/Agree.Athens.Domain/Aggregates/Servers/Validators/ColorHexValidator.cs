using System;
using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Servers.Validators
{
    public class ColorHexValidator : AbstractValidator<ColorHex>
    {
        public ColorHexValidator()
        {
            RuleFor(colorHex => colorHex.Value)
                .Length(6)
                .Must(BeHexadecimalColor);
        }

        private bool BeHexadecimalColor(string hex)
        {
            return true;
        }
    }
}