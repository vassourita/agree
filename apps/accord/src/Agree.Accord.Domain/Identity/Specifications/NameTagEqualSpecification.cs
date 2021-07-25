using System;
using System.Linq.Expressions;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Specifications
{
    /// <summary>
    /// A composite specification that checks if the account nametag (userName + tag) of a given user is equal to a given value.
    /// </summary>
    public class NameTagEqualSpecification : Specification<ApplicationUser>
    {
        public NameTagEqualSpecification(DiscriminatorTag tag, string userName)
        {
            Expression = u => u.DisplayName == userName && u.Tag == tag;
        }

        public NameTagEqualSpecification(ApplicationUser account)
        {
            Expression = u => u.DisplayName == account.DisplayName && u.Tag == account.Tag;
        }
    }
}