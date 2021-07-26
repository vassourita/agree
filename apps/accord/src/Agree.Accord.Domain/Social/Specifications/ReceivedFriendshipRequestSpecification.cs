using System;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Social.Specifications
{
    /// <summary>
    /// A specification that checks if a given friendship request has been received but not accepted yet.
    /// </summary>
    public class ReceivedFriendshipRequestSpecification : Specification<Friendship>
    {
        public ReceivedFriendshipRequestSpecification(Guid userId)
        {
            Expression = x => (x.ToId == userId && !x.Accepted);
        }
    }
}