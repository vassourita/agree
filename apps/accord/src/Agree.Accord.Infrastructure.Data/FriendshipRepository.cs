namespace Agree.Accord.Infrastructure.Data;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social;
using Agree.Accord.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// A custom <see cref="IRepository{T}"/> implementation for <see cref="Friendship"/> using Entity Framework.
/// </summary>
public class FriendshipRepository : GenericRepository<Friendship, string>, IRepository<Friendship, string>
{
    public FriendshipRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public new async Task<IEnumerable<Friendship>> GetAllAsync(Specification<Friendship> specification)
    {
        var result = await _dbContext.Set<Friendship>()
            .Where(specification.Expression)
            .Include(f => f.From)
            .Include(f => f.To)
            .ToListAsync();
        return result;
    }

    public new async Task<Friendship> GetFirstAsync(Specification<Friendship> specification)
    {
        var result = await _dbContext.Set<Friendship>()
            .Where(specification.Expression)
            .Include(f => f.From)
            .Include(f => f.To)
            .FirstOrDefaultAsync();
        return result;
    }
}