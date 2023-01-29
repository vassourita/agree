namespace Agree.Allow.Domain.Specifications;

using System;
using System.Linq.Expressions;
using Agree.SharedKernel.Data;

public class UserNameEqualSpecification : Specification<UserAccount>, IOrderedSpecification<UserAccount>
{
    public UserNameEqualSpecification(string username, bool orderByTag = false)
    {
        Expression = x
            => x.Username == username;

        if (orderByTag)
        {
            OrderingExpression = x => x.Tag;
            IsDescending = true;
        }
    }

    public Expression<Func<UserAccount, object>> OrderingExpression { get; private set; }

    public bool IsDescending { get; private set; }
}