using System;
using System.Linq;
using System.Linq.Expressions;
using Agree.Accord.Domain.Servers;
using Agree.Accord.SharedKernel.Data;

public abstract class BaseServerSpecification : AndSpecification<Server>
{
    public BaseServerSpecification(Expression<Func<Server, bool>> expression, Guid userId)
        : base(new GenericSpecification<Server>(expression), new ServerVisibleToUserSpecification(userId))
    { }
}