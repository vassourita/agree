namespace Agree.Accord.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using Agree.Accord.SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// A generic repository for the data access layer using Entity Framework.
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericRepository<T, TId> : IRepository<T, TId>
    where T : class, IEntity<TId>
{
    protected readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public Task CommitAsync() => _dbContext.SaveChangesAsync();

    public Task DeleteAsync(T entity)
    {
        try
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.FromResult(DatabaseOperationResult.Ok());
        }
        catch
        {
            return Task.FromResult(DatabaseOperationResult.Fail());
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(Specification<T> specification) => specification is PaginatedSpecification<T> paginatedSpecification
            ? await _dbContext.Set<T>()
                 .Where(specification.Expression)
                 .Skip((paginatedSpecification.Pagination.Page - 1) * paginatedSpecification.Pagination.PageSize)
                 .Take(paginatedSpecification.Pagination.PageSize)
                 .ToListAsync()
            : await _dbContext.Set<T>().Where(specification.Expression).ToListAsync();

    public async Task<T> GetFirstAsync(Specification<T> specification)
    {
        var result = await _dbContext.Set<T>().Where(specification.Expression).FirstOrDefaultAsync();
        return result;
    }

    public async Task<T> InsertAsync(T entity)
    {
        try
        {
            var response = await _dbContext.Set<T>().AddAsync(entity);
            return response.Entity;
        }
        catch (Exception e)
        {
            throw new RepositoryOperationException($"An error ocurred while inserting entity {nameof(T)}#{entity.Id}", e);
        }
    }

    public Task<T> UpdateAsync(T entity)
    {
        try
        {
            var response = _dbContext.Set<T>().Update(entity);
            return Task.FromResult(response.Entity);
        }
        catch (Exception e)
        {
            throw new RepositoryOperationException($"An error ocurred while updating entity {nameof(T)}#{entity.Id}", e);
        }
    }
}