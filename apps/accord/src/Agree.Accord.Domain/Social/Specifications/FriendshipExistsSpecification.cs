namespace Agree.Accord.Domain.Social.Specifications;

using System;
using Agree.Accord.SharedKernel.Data;

/// <summary>
/// A specification that checks if a friendship exists between two users.
/// </summary>
public class FriendshipExistsSpecification : Specification<Friendship>
{
    public FriendshipExistsSpecification(Guid from, Guid to) => Expression = x => (x.ToId == to && x.FromId == from) || (x.ToId == from && x.FromId == to);
}