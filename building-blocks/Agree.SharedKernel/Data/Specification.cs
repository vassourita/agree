namespace Agree.SharedKernel.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public abstract class Specification<T> : ISpecification<T>
{
    public Specification()
    { }

    public Expression<Func<T, bool>> Expression { get; protected set; }
}

public abstract class AndSpecification<T> : Specification<T>
{
    public Specification<T> Left { get; protected set; }
    public Specification<T> Right { get; protected set; }

    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        Left = left;
        Right = right;
        Expression = Left.Expression.And(Right.Expression);
    }
}

public abstract class OrSpecification<T> : Specification<T>
{
    public Specification<T> Left { get; protected set; }
    public Specification<T> Right { get; protected set; }

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        Left = left;
        Right = right;
        Expression = Left.Expression.Or(Right.Expression);
    }
}

public abstract class NotSpecification<T> : Specification<T>
{
    public Specification<T> Specification { get; protected set; }

    public NotSpecification(Specification<T> specification)
    {
        Specification = specification;
        Expression = Specification.Expression.Not();
    }
}

public class GenericSpecification<T> : Specification<T>
{
    public GenericSpecification(Expression<Func<T, bool>> expression)
        => Expression = expression;
}

public static class SpecificationExtensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        => left.Compose(right, Expression.AndAlso);

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        => left.Compose(right, Expression.OrElse);

    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        => Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);

    public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
    {
        // zip parameters (map from parameters of second to parameters of first)
        var map = first.Parameters
            .Select((f, i) => new { f, s = second.Parameters[i] })
            .ToDictionary(p => p.s, p => p.f);

        // replace parameters in the second lambda expression with the parameters in the first
        var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

        // create a merged lambda expression with parameters from the first expression
        return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
    }

    private class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            => _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            => new ParameterRebinder(map).Visit(exp);

        protected override Expression VisitParameter(ParameterExpression p)
            => _map.TryGetValue(p, out var replacement) ? replacement : p;
    }
}