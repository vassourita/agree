namespace Agree.Accord.Domain.Social.Specifications;

using System;
using Agree.Accord.SharedKernel.Data;

/// <summary>
/// A specification that checks if a given friendship request has been sent but not accepted yet.
/// </summary>
public class SentFriendshipRequestSpecification : Specification<Friendship>
{
    public SentFriendshipRequestSpecification(Guid userId)
        => Expression = x
        => x.FromId == userId && !x.Accepted;
}