namespace Agree.Allow.Test;

using System.Collections.Generic;
using System.Threading.Tasks;
using Agree.SharedKernel;
using Agree.SharedKernel.Data;

public class TestRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
{
    private readonly List<TEntity> _entities = new();

    public Task CommitAsync() => Task.CompletedTask;

    public Task DeleteAsync(TEntity entity)
    {
        _entities.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<TEntity>>(_entities);
    }

    public Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification)
    {
        var query = _entities.Where(specification.Expression.Compile());

        if (specification is IOrderedSpecification<TEntity> orderedSpecification && orderedSpecification.OrderingExpression != null)
        {
            query = !orderedSpecification.IsDescending
                ? query.OrderBy(orderedSpecification.OrderingExpression.Compile())
                : query.OrderByDescending(orderedSpecification.OrderingExpression.Compile());
        }

        if (specification is IPaginatedSpecification<TEntity> paginatedSpecification && paginatedSpecification.Pagination != null)
        {
            query
                .Skip((paginatedSpecification.Pagination.Page - 1) * paginatedSpecification.Pagination.PageSize)
                .Take(paginatedSpecification.Pagination.PageSize);
        }

        return Task.FromResult(query.ToList().AsEnumerable());
    }

    public Task<TEntity> GetFirstAsync(ISpecification<TEntity> specification)
    {
        var query = _entities.Where(specification.Expression.Compile());

        if (specification is IOrderedSpecification<TEntity> orderedSpecification && orderedSpecification.OrderingExpression != null)
            query = !orderedSpecification.IsDescending
                ? query.OrderBy(orderedSpecification.OrderingExpression.Compile())
                : query.OrderByDescending(orderedSpecification.OrderingExpression.Compile());

        return Task.FromResult(query.FirstOrDefault());
    }

    public Task<TEntity> InsertAsync(TEntity entity)
    {
        _entities.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        return Task.FromResult(entity);
    }
}