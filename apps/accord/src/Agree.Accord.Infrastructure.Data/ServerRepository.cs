namespace Agree.Accord.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Servers;
using Agree.Accord.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// A custom <see cref="IRepository{T}"/> implementation for <see cref="Server"/> using Entity Framework.
/// </summary>
public class ServerRepository : GenericRepository<Server, Guid>, IServerRepository
{
    public ServerRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<IEnumerable<Server>> SearchAsync(string query, Guid userId, Pagination pagination)
    {
        query = $"%{query}%";
        var result = await _dbContext.Set<Server>().FromSqlInterpolated($@"
                SELECT *
                FROM ""Servers"" AS s
                WHERE (
                    s.""Name"" ILIKE {query} OR
                    s.""Description"" ILIKE {query}
                )
                AND CASE WHEN s.""PrivacyLevel"" = 'Secret' THEN EXISTS (
                    SELECT 1
                    FROM ""ServerMembers"" AS sm
                    WHERE sm.""ServerId"" = s.""Id"" AND sm.""UserId"" = {userId}
                )
                ELSE TRUE END
            ")
            .OrderBy(s => s.CreatedAt)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
        return result;
    }
}