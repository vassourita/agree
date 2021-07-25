using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

namespace Agree.Accord.Infrastructure.Data
{
    public class FriendshipRepository : IRepository<Friendship>
    {
        private readonly ApplicationDbContext _dbContext;

        public FriendshipRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public Task<IResult> DeleteAsync(Friendship entity)
        {
            try
            {
                _dbContext.Set<Friendship>().Remove(entity);
                return Task.FromResult(DatabaseOperationResult.Ok());
            }
            catch
            {
                return Task.FromResult(DatabaseOperationResult.Fail());
            }
        }

        public async Task<IEnumerable<Friendship>> GetAllAsync(Specification<Friendship> specification)
        {
            var result = await _dbContext.Set<Friendship>().Where(specification.Expression).Include(f => f.From).Include(f => f.To).ToListAsync();
            return result;
        }

        public async Task<Friendship> GetFirstAsync(Specification<Friendship> specification)
        {
            var result = await _dbContext.Set<Friendship>().Where(specification.Expression).Include(f => f.From).Include(f => f.To).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IResult> InsertAsync(Friendship entity)
        {
            try
            {
                await _dbContext.Set<Friendship>().AddAsync(entity);
                return DatabaseOperationResult.Ok();
            }
            catch
            {
                return DatabaseOperationResult.Fail();
            }
        }

        public Task<IResult> UpdateAsync(Friendship entity)
        {
            try
            {
                _dbContext.Set<Friendship>().Update(entity);
                return Task.FromResult(DatabaseOperationResult.Ok());
            }
            catch
            {
                return Task.FromResult(DatabaseOperationResult.Fail());
            }
        }
    }
}