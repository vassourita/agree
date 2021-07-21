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
        private readonly string _email;

        public EmailEqualSpecification(string email)
        {
            Expression = x => x.Email == email;
        }

        public EmailEqualSpecification(UserAccount account)
        {
            Expression = x => x.Email == account.Email;
        }
    }
}