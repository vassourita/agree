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
}