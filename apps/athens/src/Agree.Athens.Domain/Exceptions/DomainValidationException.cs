using System.Collections.Generic;
using System.Linq;
using Agree.Athens.SharedKernel;
using FluentValidation.Results;

namespace Agree.Athens.Domain.Exceptions
{
    public class DomainValidationException : BaseDomainException
    {
        private readonly IEnumerable<ValidationError> Errors;
        private readonly Validatable Item;

        public DomainValidationException(Validatable item) : base($"Validation failed")
        {
            Errors = item.ValidationResult.Errors.Select(e => new ValidationError(e));
            Item = item;
        }

        public IEnumerable<ValidationError> GetErrors() => Errors.ToList().AsReadOnly();

        public class ValidationError
        {
            public ValidationError(ValidationFailure failure)
            {
                Property = failure.PropertyName;
                Message = failure.ErrorMessage;
            }
            public string Property { get; private set; }
            public string Message { get; private set; }
        }
    }
}