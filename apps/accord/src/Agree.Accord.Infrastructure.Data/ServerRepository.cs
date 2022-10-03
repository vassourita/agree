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

    public async Task<IEnumerable<Server>> SearchAsync(string query, Pagination pagination)
    {
        var result = await _dbContext.Set<Server>().FromSqlRaw($@"
                SELECT *
                FROM ""Servers""
                WHERE ""Name"" ILIKE '%{query}%' OR ""Description"" ILIKE '%{query}%'
            ")
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
        return result;
    }
}