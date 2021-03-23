using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Servers.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name)
                .MinimumLength(1).WithMessage("Category name must have at least 1 character")
                .MaximumLength(30).WithMessage("Category name must not have more than 30 characters");
        }
    }
}