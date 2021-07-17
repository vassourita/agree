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
        public UserNameEqualSpecification(string userName)
            : base(account => account.UserName == userName)
        { }

        public UserNameEqualSpecification(UserAccount acc)
            : base(account => account.UserName == acc.UserName)
        { }
    }
}