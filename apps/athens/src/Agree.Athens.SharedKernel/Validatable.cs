using FluentValidation;
using FluentValidation.Results;

namespace Agree.Athens.SharedKernel
{
    public abstract class Validatable
    {
        public bool IsValid { get; private set; }
        public bool IsInvalid => !IsValid;
        public ValidationResult ValidationResult { get; private set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            IsValid = ValidationResult.IsValid;
            return IsValid;
        }

        public bool Validate<TModel>(AbstractValidator<TModel> validator)
            where TModel : Validatable
        {
            ValidationResult = validator.Validate(this as TModel);
            IsValid = ValidationResult.IsValid;
            return IsValid;
        }

        public void AddError(string propertyName, string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(propertyName, message));
            IsValid = false;
        }

        public void AddError(string propertyName, string message, object attemptedValue)
        {
            ValidationResult.Errors.Add(new ValidationFailure(propertyName, message, attemptedValue));
            IsValid = false;
        }
    }
}