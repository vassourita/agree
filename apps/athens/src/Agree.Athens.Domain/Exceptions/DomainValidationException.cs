using System.Collections.Generic;
using System.Linq;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Exceptions
{
    public class DomainValidationException : BaseDomainException
    {
        private readonly IEnumerable<ValidationError> Errors;
        private readonly Validatable Item;

        public DomainValidationException(Validatable item) : base($"Validation failed")
        {
            Errors = item.ValidationResult.Errors.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage));
            Item = item;
        }

        public IEnumerable<ValidationError> GetErrors() => Errors.ToList().AsReadOnly();

        public record ValidationError(string Property, string Message);
    }
}