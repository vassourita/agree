using System;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Social.Specifications
{
    public class FriendshipExistsSpecification : Specification<Friendship>
    {
        public FriendshipExistsSpecification(Guid from, Guid to)
        {
            Expression = x => (x.ToId == to && x.FromId == from) || (x.ToId == from && x.FromId == to);
        }
    }
}