using System;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Social.Specifications
{
    public class SentFriendshipRequestSpecification : Specification<Friendship>
    {
        public SentFriendshipRequestSpecification(Guid userId)
        {
            Expression = x => (x.FromId == userId && !x.Accepted);
        }
    }
}