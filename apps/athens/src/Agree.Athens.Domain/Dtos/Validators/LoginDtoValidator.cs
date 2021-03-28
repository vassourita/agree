using System;
using FluentValidation;

namespace Agree.Athens.Domain.Dtos.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(dto => dto.GrantType)
                .Must(BeValidGrantType).WithMessage("Grant type must be one of 'password' or 'refresh_token'");
        }

        private bool BeValidGrantType(string arg)
        {
            return arg == "password" || arg == "refresh_token";
        }
    }
}