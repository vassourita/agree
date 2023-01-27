namespace Agree.Allow.Domain.Specifications;

using System;
using Agree.SharedKernel.Data;

/// <summary>
/// A specification that checks if the account id of a given user is equal to a given value.
/// </summary>
public class UserIdEqualSpecification : Specification<UserAccount>
{
    public UserIdEqualSpecification(Guid id)
        => Expression = u
        => u.Id == id;

    public UserIdEqualSpecification(UserAccount account)
        => Expression = u
        => u.Id == account.Id;
}