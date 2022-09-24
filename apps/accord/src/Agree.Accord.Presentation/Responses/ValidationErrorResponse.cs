namespace Agree.Accord.Presentation.Responses;

using Agree.Accord.SharedKernel;

/// <summary>
/// The response to a validation error.
/// </summary>
public class ValidationErrorResponse
{
    public ValidationErrorResponse(ErrorList errors) => Errors = errors;
    public ErrorList Errors { get; private set; }
    public string Message => "One or more validation errors occurred.";
}