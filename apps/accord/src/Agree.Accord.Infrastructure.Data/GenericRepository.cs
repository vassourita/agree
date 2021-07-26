using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

namespace Agree.Accord.Infrastructure.Data
{
    /// <summary>
    /// A generic repository for the data access layer using Entity Framework.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

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

        public async Task<IEnumerable<T>> GetAllAsync(Specification<T> specification)
        {
            var result = await _dbContext.Set<T>().Where(specification.Expression).ToListAsync();
            return result;
        }

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
}