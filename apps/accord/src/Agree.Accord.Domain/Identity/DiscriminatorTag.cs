using System.Collections.Generic;
using System.Linq;
using Agree.Accord.SharedKernel;

namespace Agree.Accord.Domain.Identity
{
    /// <summary>
    /// Represents a four digit number used to differentiate users with the same name.
    /// </summary>
    public class DiscriminatorTag : ValueObject
    {
        /// <summary>
        /// The tag value as a ushort. Will not have four digits if it starts with zeros.
        /// </summary>
        /// <value>Gets the tag value as a ushort.</value>
        public ushort Value { get; private set; }

        /// <summary>
        /// Returns tag value as a string. Will always be a numeric string and have a length of four.
        /// </summary>
        /// <returns>The tag value as a string.</returns>
        public override string ToString() => Value.ToString().PadLeft(4, '0');

        private DiscriminatorTag(ushort value)
        {
            Value = value;
        }

        /// <summary>
        /// Tries to parse a object into a <c>DiscriminatorTag</c>.
        /// </summary>
        /// <param name="value">The value to be parsed</param>
        /// <param name="tag">Outputs the parsed value if <c>value</c> was converted succesfully; otherwise, a zero-filled <c>DiscriminatorTag</c></param>
        /// <returns><c>true</c> if <c>value</c> converted succesfully; otherwise, <c>false</c>.</returns>
        public static bool TryParse(object value, out DiscriminatorTag tag)
        {
            tag = new DiscriminatorTag(0);
            var strValue = value.ToString();

            if (string.IsNullOrEmpty(strValue) || strValue.Length > 4 || !strValue.All(char.IsDigit))
            {
                return false;
            }

            if (ushort.TryParse(strValue.PadLeft(4, '0'), out var parsedValue))
            {
                tag = new DiscriminatorTag(parsedValue);
                return true;
            }

            return false;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}