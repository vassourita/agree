using System;
using System.ComponentModel.DataAnnotations;
using Agree.Athens.Domain.Aggregates.Account.Validators;
using Agree.Athens.SharedKernel;
using FluentValidation;

namespace Agree.Athens.Domain.Aggregates.Account
{
    public class UserTag : Validatable
    {
        [MaxLength(4)]
        [MinLength(4)]
        public string Value { get; set; }

        public UserTag(string tag)
        {
            Value = tag;

            Validate(this, new UserTagValidator());
        }

        public override string ToString()
        {
            return Value;
        }
    }
}