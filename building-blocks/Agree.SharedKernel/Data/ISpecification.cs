namespace Agree.SharedKernel.Data;

using System.Linq.Expressions;

public interface ISpecification<T>
{
    public Expression<Func<T, bool>> Expression { get; }
}

public interface IPaginatedSpecification<T> : ISpecification<T>
{
    public IPagination Pagination { get; }
}

public interface IOrderedSpecification<T> : ISpecification<T>
{
    public Expression<Func<T, object>> OrderingExpression { get; }
    public bool IsDescending { get; }
}