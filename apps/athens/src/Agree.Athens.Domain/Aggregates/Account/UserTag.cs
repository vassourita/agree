using Agree.Athens.Domain.Aggregates.Account.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Account
{
    public class UserTag : Validatable
    {
        public string _value { get; private set; }

        public UserTag(string tag)
        {
            _value = tag;

            Validate(this, new UserTagValidator());
        }

        public override string ToString()
        {
            return _value;
        }
    }
}