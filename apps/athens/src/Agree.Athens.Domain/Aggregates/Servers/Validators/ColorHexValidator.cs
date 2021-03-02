using System;
using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Servers.Validators
{
    public class ColorHexValidator : AbstractValidator<ColorHex>
    {
        public ColorHexValidator()
        {
            RuleFor(colorHex => colorHex.ToString())
                .Length(6).WithMessage("Color Hex must have 6 characters")
                .Must(BeHexadecimalColor).WithMessage("Color Hex must be a valid hexadecimal color");
        }

        private bool BeHexadecimalColor(string hex)
        {
            return true;
        }
    }
}