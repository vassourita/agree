using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class ColorHex : Validatable
    {
        public string Value { get; private set; }

        public ColorHex(string hex)
        {
            Value = hex;

            Validate(this, new ColorHexValidator());
        }

        public override string ToString()
        {
            return $"ColorHex [Value={Value}]";
        }
    }
}