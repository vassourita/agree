using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Agree.Accord.SharedKernel
{
    public class ErrorList : Dictionary<string, IEnumerable<string>>
    {
        public ErrorList AddError(string propertyName, string errorMessage)
        {
            if (this[propertyName] == null)
            {
                this[propertyName] = new List<string>();
            }
            this[propertyName].Append(errorMessage);

            return this;
        }

        public ErrorList() { }
    }

    public static class ValidationResultExtensions
    {
        public static ErrorList ToErrorList(this IEnumerable<ValidationResult> validationResults)
            => validationResults.Aggregate(new ErrorList(), (list, result) =>
            {
                var property = result.ErrorMessage.Split(' ')[0];
                if (list[property] == null)
                {
                    list[property] = new List<string>();
                }
                list[property].Append(result.ErrorMessage);
                return list;
            });
    }
}