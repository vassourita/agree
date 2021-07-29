using System;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Social.Specifications
{
    public class DirectMessageFromOrToFriendSpecification : Specification<DirectMessage>
    {
        public DirectMessageFromOrToFriendSpecification(Guid requesterId, Guid friendId)
        {
            Expression = x => (x.From.Id == friendId && x.To.Id == requesterId) || (x.To.Id == friendId && x.From.Id == requesterId);
        }
    }
}