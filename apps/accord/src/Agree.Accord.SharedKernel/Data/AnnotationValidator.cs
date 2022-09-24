namespace Agree.Accord.SharedKernel.Data;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public static class AnnotationValidator
{
    public static AnnotationValidatorResult<T> TryValidate<T>(T obj)
    {
        var validationResult = new List<ValidationResult>();
        var validationCtx = new ValidationContext(obj, null, null);
        return Validator.TryValidateObject(obj, validationCtx, validationResult, true)
            ? AnnotationValidatorResult<T>.Ok(obj)
            : AnnotationValidatorResult<T>.Fail(validationResult);
    }
}