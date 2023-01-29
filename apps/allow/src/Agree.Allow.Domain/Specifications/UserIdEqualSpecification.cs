namespace Agree.Allow.Domain.Specifications;

using System;
using Agree.SharedKernel.Data;

public class UserIdEqualSpecification : Specification<UserAccount>
{
    public UserIdEqualSpecification(Guid id)
        => Expression = x
        => x.Id == id;

    public UserIdEqualSpecification(UserAccount account)
        => Expression = x
        => x.Id == account.Id;
}