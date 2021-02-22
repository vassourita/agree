using FluentValidation;
using FluentValidation.Results;

namespace Agree.Athens.SharedKernel
{
    public abstract class Validatable
    {
        public bool IsValid { get; private set; }
        public bool IsInvalid => !IsValid;
        public ValidationResult ValidationResult { get; private set; }

        protected bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            IsValid = ValidationResult.IsValid;
            return IsValid;
        }
    }
}