namespace Agree.Allow.Domain.Specifications;

using System.Linq.Expressions;
using Agree.SharedKernel.Data;

public class NameTagEqualSpecification : Specification<UserAccount>, IOrderedSpecification<UserAccount>
{
    public NameTagEqualSpecification(DiscriminatorTag tag, string userName, bool orderByTag = false)
    {
        Expression = x
            => x.Username == userName && x.Tag == tag;

        if (orderByTag)
        {
            OrderingExpression = x => x.Tag;
            IsDescending = true;
        }
    }

    public NameTagEqualSpecification(UserAccount account)
        => Expression = x
        => x.Username == account.Username && x.Tag == account.Tag;

    public Expression<Func<UserAccount, object>> OrderingExpression { get; private set; }
    public bool IsDescending { get; private set; }
}