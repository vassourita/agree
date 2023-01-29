namespace Agree.Allow.Domain.Specifications;

using System;
using Agree.SharedKernel.Data;

public class UserIdEqualSpecification : Specification<UserAccount>
{
    public UserIdEqualSpecification(Guid id)
        => Expression = u
        => u.Id == id;

    public UserIdEqualSpecification(UserAccount account)
        => Expression = u
        => u.Id == account.Id;
}