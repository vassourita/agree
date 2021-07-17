using System;
using System.Linq.Expressions;

namespace Agree.Accord.SharedKernel.Data
{
    public abstract class Specification<T>
    {
        public Expression<Func<T, bool>> Expression { get; }

        public Specification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }

    public abstract class AndSpecification<T> : Specification<T>
    {
        public Specification<T> Left { get; }
        public Specification<T> Right { get; }
        public AndSpecification(Specification<T> left, Specification<T> right)
            : base(expression => left.IsSatisfiedBy(expression) && right.IsSatisfiedBy(expression))
        {
            Left = left;
            Right = right;
        }
    }

    public abstract class OrSpecification<T> : Specification<T>
    {
        public Specification<T> Left { get; }
        public Specification<T> Right { get; }
        public OrSpecification(Specification<T> left, Specification<T> right)
            : base(expression => left.IsSatisfiedBy(expression) || right.IsSatisfiedBy(expression))
        {
            Left = left;
            Right = right;
        }
    }

    public abstract class NotSpecification<T> : Specification<T>
    {
        public Specification<T> InnerSpecification { get; }
        public NotSpecification(Specification<T> innerSpecification)
            : base(expression => !innerSpecification.IsSatisfiedBy(expression))
        {
            InnerSpecification = innerSpecification;
        }
    }
}