using Agree.Accord.SharedKernel;

namespace Agree.Accord.Presentation.Responses
{
    public class ValidationErrorResponse
    {
        public ErrorList _errors { get; private set; }

        public string Message => "One or more validation errors occurred.";

        public ValidationErrorResponse(ErrorList errors)
        {
            _errors = errors;
        }
    }
}