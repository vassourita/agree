using System;
using System.Linq.Expressions;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Specifications
{
    /// <summary>
    /// A composite specification that checks if the account nametag (userName + tag) of a given user is equal to a given value.
    /// </summary>
    public class NameTagEqualSpecification : AndSpecification<UserAccount>
    {
        public NameTagEqualSpecification(DiscriminatorTag tag, string userName)
            : base(new TagEqualSpecification(tag), new UserNameEqualSpecification(userName))
        { }

        public NameTagEqualSpecification(UserAccount account)
            : base(new TagEqualSpecification(account.Tag), new UserNameEqualSpecification(account.UserName))
        { }
    }
}