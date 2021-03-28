using System;
using System.Linq;
using FluentValidation;

namespace Agree.Athens.Domain.Dtos.Validators
{
    public class SearchServerDtoValidator : AbstractValidator<SearchServerDto>
    {
        public SearchServerDtoValidator()
        {
            RuleFor(dto => dto.OrderBy)
                .Must(BeValidOrderOption).WithMessage("Ordering must be one of 'creationdate_desc', 'creationdate' or 'popular'");

            RuleFor(dto => dto.Query)
                .NotEmpty().WithMessage("Query must not be empty");

            RuleFor(dto => dto.PageLimit)
                .GreaterThanOrEqualTo(1).WithMessage("Page must not have less than 1 result")
                .LessThanOrEqualTo(50).WithMessage("Page must not have more than 50 results");

            RuleFor(dto => dto.Page)
                .GreaterThanOrEqualTo(1).WithMessage("Page must be greater than 0");
        }

        private bool BeValidOrderOption(string arg)
        {
            var options = new[] { "creationdate_desc", "creationdate", "popular" };
            return options.Any(opt => opt == arg);
        }
    }
}