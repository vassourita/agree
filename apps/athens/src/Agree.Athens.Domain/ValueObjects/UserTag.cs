using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Agree.Athens.Domain.Exceptions;
using System.Linq;

namespace Agree.Athens.Domain.ValueObjects
{
    public class UserTag : ValueObject
    {
        [MaxLength(4)]
        [MinLength(4)]
        private string _value { get; set; }

        public UserTag(int tag)
        {
            _value = ValidateAndFormatTag(tag.ToString());
        }

        public UserTag(string tag)
        {
            if (tag == null) throw InvalidUserTagException.NullOrEmpty();
            _value = ValidateAndFormatTag(tag);
        }

        private string ValidateAndFormatTag(string tag)
        {
            tag = tag.Trim();
            if (!int.TryParse(tag, out _))
            {
                throw InvalidUserTagException.NotNumeric();
            }
            if (string.IsNullOrEmpty(tag))
            {
                throw InvalidUserTagException.NullOrEmpty();
            }
            if (tag.Length > 4)
            {
                throw InvalidUserTagException.MaxLengthExceeded();
            }
            if (tag.All(c => c == '0'))
            {
                throw InvalidUserTagException.AllZeros();
            }
            if (tag.Length < 4)
            {
                return tag.PadLeft(4, '0');
            }
            return tag;
        }

        public static UserTag GenerateRandomTag()
        {
            var random = new Random().Next(1, 9999);
            return new UserTag(random);
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