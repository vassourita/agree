namespace Agree.SharedKernel.Data;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class AnnotationValidatorResult<T> : Result<T, List<ValidationResult>>
{
    private AnnotationValidatorResult(T data) : base(data)
    { }
    private AnnotationValidatorResult(List<ValidationResult> error) : base(error)
    { }

    public static AnnotationValidatorResult<T> Ok(T data) => new(data);
    public static AnnotationValidatorResult<T> Fail(List<ValidationResult> data) => new(data);
}