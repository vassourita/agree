using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.SharedKernel;

namespace Agree.Accord.Domain.Identity.Results
{
    public class CreateAccountResult : Result<UserAccount, List<ValidationResult>>
    {
        private CreateAccountResult(UserAccount data) : base(data)
        {
        }
        private CreateAccountResult(List<ValidationResult> error) : base(error)
        {
        }

        public static CreateAccountResult Ok(UserAccount data) => new(data);
        public static CreateAccountResult Fail(List<ValidationResult> data) => new(data);
    }
}