namespace Agree.Accord.Infrastructure.Data;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// A generic repository for the data access layer using Entity Framework.
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericRepository<T> : IRepository<T>
    where T : class
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public Task CommitAsync() => _dbContext.SaveChangesAsync();

    public Task<IResult> DeleteAsync(T entity)
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

    public async Task<IResult> InsertAsync(T entity)
    {
        try
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return DatabaseOperationResult.Ok();
        }
        catch
        {
            return DatabaseOperationResult.Fail();
        }
    }

    public Task<IResult> UpdateAsync(T entity)
    {
        try
        {
            _dbContext.Set<T>().Update(entity);
            return Task.FromResult(DatabaseOperationResult.Ok());
        }
        catch
        {
            return Task.FromResult(DatabaseOperationResult.Fail());
        }
    }
}