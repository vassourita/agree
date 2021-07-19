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
            : base(account => account.Id == id)
        { }

        public IdEqualSpecification(UserAccount acc)
            : base(account => account.Id == acc.Id)
        { }
    }
}