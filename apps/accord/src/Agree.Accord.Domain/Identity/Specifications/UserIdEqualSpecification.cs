namespace Agree.Accord.Domain.Identity.Specifications;

using System;
using Agree.Accord.SharedKernel.Data;

/// <summary>
/// A specification that checks if the account id of a given user is equal to a given value.
/// </summary>
public class UserIdEqualSpecification : Specification<UserAccount>
{
    public UserIdEqualSpecification(Guid id) => Expression = x => x.Id == id;

    public UserIdEqualSpecification(UserAccount account) => Expression = x => x.Id == account.Id;
}