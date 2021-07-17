using System;
using System.Linq.Expressions;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Specifications
{
    /// <summary>
    /// A specification that checks if the account email address of a given user is equal to a given value.
    /// </summary>
    public class EmailEqualSpecification : Specification<UserAccount>
    {
        public EmailEqualSpecification(string email)
            : base(account => account.Email == email)
        { }

        public EmailEqualSpecification(UserAccount acc)
            : base(account => account.Email == acc.Email)
        { }
    }
}