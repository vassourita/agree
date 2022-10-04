using System;
using System.Linq;
using System.Linq.Expressions;
using Agree.Accord.Domain.Servers;
using Agree.Accord.SharedKernel.Data;

public abstract class BaseServerPaginatedSpecification : PaginatedAndSpecification<Server>
{
    public BaseServerPaginatedSpecification(Expression<Func<Server, bool>> expression, Guid userId, IPagination pagination)
        : base(new GenericSpecification<Server>(expression), new ServerVisibleToUserSpecification(userId), pagination)
    { }
}