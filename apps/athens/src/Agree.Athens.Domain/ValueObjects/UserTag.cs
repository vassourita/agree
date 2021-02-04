using System.Collections.Generic;
using Agree.Athens.Domain.Exceptions;

namespace Agree.Athens.Domain.ValueObjects
{
    public class UserTag : ValueObject
    {
        private string _value { get; set; }

        public UserTag(int tag)
        {
            if (tag.ToString().Length > 4)
            {
                throw new InvalidUserTagException();
            }
            if (tag.ToString().Length < 4)
            {
                _value = tag.ToString().PadLeft(4, '0');
            }
            else
            {
                _value = tag.ToString();
            }
        }

        public UserTag(string tag)
        {
            if (tag.Length > 4)
            {
                throw new InvalidUserTagException();
            }
            if (tag.Length < 4)
            {
                _value = tag.PadLeft(4, '0');
            }
            else
            {
                _value = tag;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}