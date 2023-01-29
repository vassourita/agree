namespace Agree.SharedKernel.Data;

using System.Linq.Expressions;

public class SpecificationVisitor<TDbModel, TEntity, TId> : ExpressionVisitor
    where TEntity : IEntity<TId>
    where TDbModel : class
{
    private readonly ParameterExpression _parameter;

    public SpecificationVisitor(ParameterExpression parameter)
    {
        _parameter = parameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return _parameter;
    }
}