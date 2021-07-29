using System;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Social.Specifications
{
    public class DirectMessageFromOrToFriendPaginatedSpecification : PaginatedSpecification<DirectMessage>
    {
        public DirectMessageFromOrToFriendPaginatedSpecification(Guid requesterId, Guid friendId, IPagination pagination) : base(pagination)
        {
            Expression = x => (x.From.Id == friendId && x.To.Id == requesterId) || (x.To.Id == friendId && x.From.Id == requesterId);
        }
    }

    public class DirectMessageFromOrToFriendSpecification : Specification<DirectMessage>
    {
        public DirectMessageFromOrToFriendSpecification(Guid requesterId, Guid friendId)
        {
            Expression = x => (x.From.Id == friendId && x.To.Id == requesterId) || (x.To.Id == friendId && x.From.Id == requesterId);
        }
    }
}