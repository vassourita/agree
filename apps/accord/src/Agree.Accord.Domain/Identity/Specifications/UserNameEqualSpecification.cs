using System;
using System.Linq.Expressions;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Specifications
{
    /// <summary>
    /// A specification that checks if the account name of a given user is equal to a given value.
    /// </summary>
    public class UserNameEqualSpecification : Specification<UserAccount>
    {
        private readonly string _userName;

        public UserNameEqualSpecification(string userName)
        {
            Expression = u => u.UserName == userName;
        }

        public UserNameEqualSpecification(UserAccount account)
        {
            Expression = u => u.UserName == account.UserName;
        }
    }
}