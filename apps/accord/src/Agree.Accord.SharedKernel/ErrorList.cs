using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Agree.Accord.SharedKernel
{
    public class ErrorList : Dictionary<string, List<string>>
    {
        public ErrorList AddError(string propertyName, string errorMessage)
        {
            try
            {
                this[propertyName].Add(errorMessage);
                return this;
            }
            catch (KeyNotFoundException)
            {
                this[propertyName] = new List<string>();
                this[propertyName].Add(errorMessage);
                return this;
            }
        }

        public ErrorList() { }
    }

    public static class ValidationResultExtensions
    {
        public static ErrorList ToErrorList(this IEnumerable<ValidationResult> validationResults)
            => validationResults.Aggregate(new ErrorList(), (list, result) =>
            {
                var property = result.ErrorMessage.Split(' ')[0];
                return list.AddError(property, result.ErrorMessage);
            });
    }
}