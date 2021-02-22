using Agree.Athens.Domain.Aggregates.Account.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Account
{
    public class UserTag : Validatable
    {
        public string Value { get; set; }

        public UserTag(string tag)
        {
            Value = tag;

            Validate(this, new UserTagValidator());
        }

        public override string ToString()
        {
            return $"UserTag [Value={Value}]";
        }
    }
}