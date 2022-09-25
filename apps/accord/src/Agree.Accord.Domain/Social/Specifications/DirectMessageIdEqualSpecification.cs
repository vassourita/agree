namespace Agree.Accord.Domain.Social.Specifications;

using System;
using Agree.Accord.SharedKernel.Data;

/// <summary>
/// A specification that checks if the message id of a given message is equal to a given value.
/// </summary>
public class DirectMessageIdEqualSpecification : Specification<DirectMessage>
{
    public DirectMessageIdEqualSpecification(Guid id) => Expression = x => x.Id == id;

    public DirectMessageIdEqualSpecification(DirectMessage message)
        => Expression = x
        => x.Id == message.Id;
}