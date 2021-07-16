using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agree.Accord.SharedKernel.Data
{
    public static class AnnotationValidator
    {
        public static AnnotationValidatorResult<T> TryValidate<T>(T obj)
        {
            var validationResult = new List<ValidationResult>();
            var validationCtx = new ValidationContext(obj, null, null);
            if (Validator.TryValidateObject(obj, validationCtx, validationResult, true))
            {
                return AnnotationValidatorResult<T>.Ok(obj);
            }
            return AnnotationValidatorResult<T>.Fail(validationResult);
        }
    }
}