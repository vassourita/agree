using System;
using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Servers.Validators
{
    public class ColorHexValidator : AbstractValidator<ColorHex>
    {
        public ColorHexValidator()
        {
            RuleFor(colorHex => colorHex.Value)
                .Length(6).WithMessage(c => $"{c} must have 6 characters")
                .Must(BeHexadecimalColor).WithMessage(c => $"{c} must be a valid hexadecimal color");
        }

        private bool BeHexadecimalColor(string hex)
        {
            return true;
        }
    }
}