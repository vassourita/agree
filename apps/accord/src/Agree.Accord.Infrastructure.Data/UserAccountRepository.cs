namespace Agree.Accord.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// A <see cref="IUserAccountRepository"/> implementation using Entity Framework.
/// </summary>
public class UserAccountRepository : GenericRepository<UserAccount, Guid>, IUserAccountRepository
{
    public UserAccountRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<IEnumerable<UserAccount>> SearchAsync(string query, IPagination pagination)
    {
        var result = await _dbContext.Set<UserAccount>().FromSqlRaw($@"
                SELECT *
                FROM ""AspNetUsers""
                WHERE (""DisplayName"" || '#' || LPAD(""Tag""::varchar(4), 4, '0')) ILIKE '%{query}%'
            ")
            .OrderBy(u => u.CreatedAt)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
        return result;
    }
}