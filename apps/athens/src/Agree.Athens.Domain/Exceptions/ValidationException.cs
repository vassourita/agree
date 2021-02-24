using System;
using System.Collections.Generic;
using System.Linq;
using Agree.Athens.SharedKernel;
using FluentValidation.Results;

namespace Agree.Athens.Domain.Exceptions
{
    public class ValidationException : BaseDomainException
    {
        private readonly IEnumerable<ValidationFailure> Errors;

        public ValidationException(Validatable item) : base($"Validation for {item} failed")
        {
            Errors = item.ValidationResult.Errors;
        }

        public IEnumerable<ValidationFailure> GetErrors() => Errors.ToList().AsReadOnly();
    }
}