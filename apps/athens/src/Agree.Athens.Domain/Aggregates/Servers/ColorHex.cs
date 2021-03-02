using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class ColorHex : Validatable
    {
        public string _value { get; private set; }

        public ColorHex(string hex)
        {
            _value = hex;

            Validate(this, new ColorHexValidator());
        }

        public override string ToString()
        {
            return _value;
        }
    }
}