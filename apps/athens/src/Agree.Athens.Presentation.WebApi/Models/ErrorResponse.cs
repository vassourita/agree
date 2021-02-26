using System.Collections.Generic;
using Agree.Athens.Domain.Exceptions;
using static Agree.Athens.Domain.Exceptions.DomainValidationException;

namespace Agree.Athens.Presentation.WebApi.Models
{
    public class ErrorResponse : Response
    {
        public ErrorResponse(string message, IEnumerable<ValidationError> errors) : base(message)
        {
            Errors = errors;
        }

        public IEnumerable<ValidationError> Errors { get; set; }

        public static ErrorResponse FromException(DomainValidationException exception)
            => new ErrorResponse(exception.Message, exception.GetErrors());

    }
}