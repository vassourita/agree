using System;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Social.Specifications
{
    public class ReceivedFriendshipRequestSpecification : Specification<Friendship>
    {
        public ReceivedFriendshipRequestSpecification(Guid userId)
        {
            Expression = x => (x.ToId == userId && !x.Accepted);
        }
    }
}