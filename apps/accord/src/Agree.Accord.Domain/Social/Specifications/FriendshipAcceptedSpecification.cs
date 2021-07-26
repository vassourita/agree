using System;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Social.Specifications
{
    /// <summary>
    /// A specification that checks if a friendship has been accepted. 
    /// </summary>
    public class FriendshipAcceptedSpecification : Specification<Friendship>
    {
        public FriendshipAcceptedSpecification(Guid userId)
        {
            Expression = x => (x.ToId == userId && x.Accepted) || (x.FromId == userId && x.Accepted);
        }
    }
}