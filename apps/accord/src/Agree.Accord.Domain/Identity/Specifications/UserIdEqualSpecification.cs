using System;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Specifications
{
    /// <summary>
    /// A specification that checks if the account id of a given user is equal to a given value.
    /// </summary>
    public class UserIdEqualSpecification : Specification<ApplicationUser>
    {
        public UserIdEqualSpecification(Guid id)
        {
            Expression = x => x.Id == id;
        }

        public UserIdEqualSpecification(ApplicationUser account)
        {
            Expression = x => x.Id == account.Id;
        }
    }
}