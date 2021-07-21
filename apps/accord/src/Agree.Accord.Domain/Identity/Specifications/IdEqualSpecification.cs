using System;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Specifications
{
    /// <summary>
    /// A specification that checks if the account id of a given user is equal to a given value.
    /// </summary>
    public class IdEqualSpecification : Specification<UserAccount>
    {
        public IdEqualSpecification(Guid id)
        {
            Expression = x => x.Id == id;
        }

        public IdEqualSpecification(UserAccount account)
        {
            Expression = x => x.Id == account.Id;
        }
    }
}