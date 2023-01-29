namespace Agree.SharedKernel.Data;

using System.Linq.Expressions;

public class SpecificationConverter<TDbModel, TEntity, TId>
    where TEntity : IEntity<TId>
    where TDbModel : class
{
    public Expression<Func<TDbModel, bool>> Convert(ISpecification<TEntity> specification)
    {
        var parameter = Expression.Parameter(typeof(TDbModel), "x");
        var body = new SpecificationVisitor<TDbModel, TEntity, TId>(parameter).Visit(specification.Expression);
        return Expression.Lambda<Func<TDbModel, bool>>(body, parameter);
    }

    public Expression<Func<TDbModel, object>> Convert(IOrderedSpecification<TEntity> specification)
    {
        var parameter = Expression.Parameter(typeof(TDbModel), "x");
        var body = new SpecificationVisitor<TDbModel, TEntity, TId>(parameter).Visit(specification.OrderingExpression);
        return Expression.Lambda<Func<TDbModel, object>>(body, parameter);
    }
}