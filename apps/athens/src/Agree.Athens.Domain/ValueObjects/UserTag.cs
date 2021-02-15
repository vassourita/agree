using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Agree.Athens.Domain.Exceptions;

namespace Agree.Athens.Domain.ValueObjects
{
    public class UserTag : ValueObject
    {
        [MaxLength(4)]
        [MinLength(4)]
        private string _value { get; set; }

        public UserTag(int tag)
        {
            var tagString = tag.ToString();
            if (tagString.Length > 4)
            {
                throw new InvalidUserTagException();
            }
            if (tagString.Length < 4)
            {
                _value = tagString.PadLeft(4, '0');
            }
            else
            {
                _value = tagString;
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