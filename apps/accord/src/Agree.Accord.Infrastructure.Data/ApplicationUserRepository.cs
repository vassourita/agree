using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

namespace Agree.Accord.Infrastructure.Data
{
    /// <summary>
    /// A <see cref="IApplicationUserRepository"/> implementation using Entity Framework.
    /// </summary>
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationUserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public Task<IResult> DeleteAsync(ApplicationUser entity)
        {
            try
            {
                _dbContext.Set<ApplicationUser>().Remove(entity);
                return Task.FromResult(DatabaseOperationResult.Ok());
            }
            catch
            {
                return Task.FromResult(DatabaseOperationResult.Fail());
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync(Specification<ApplicationUser> specification)
        {
            var result = await _dbContext.Set<ApplicationUser>().Where(specification.Expression).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ApplicationUser>> SearchAsync(string query)
        {
            var result = await _dbContext.Set<ApplicationUser>().FromSqlRaw($@"
                SELECT
                	""Id"", ""DisplayName"", ""Tag"", ""UserName"", ""NormalizedUserName"", ""Email"",
                    ""NormalizedEmail"", ""EmailConfirmed"", ""PasswordHash"", ""SecurityStamp"",
                    ""ConcurrencyStamp"", ""PhoneNumber"", ""PhoneNumberConfirmed"", ""TwoFactorEnabled"",
                    ""LockoutEnd"", ""LockoutEnabled"", ""AccessFailedCount""
                FROM ""AspNetUsers""
                WHERE (""DisplayName"" || '#' || LPAD(""Tag""::varchar(4), 4, '0')) ILIKE '%{query}%';
            ").ToListAsync();
            return result;
        }

        public async Task<ApplicationUser> GetFirstAsync(Specification<ApplicationUser> specification)
        {
            var result = await _dbContext.Set<ApplicationUser>().Where(specification.Expression).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IResult> InsertAsync(ApplicationUser entity)
        {
            try
            {
                await _dbContext.Set<ApplicationUser>().AddAsync(entity);
                return DatabaseOperationResult.Ok();
            }
            catch
            {
                return DatabaseOperationResult.Fail();
            }
        }

        public Task<IResult> UpdateAsync(ApplicationUser entity)
        {
            try
            {
                _dbContext.Set<ApplicationUser>().Update(entity);
                return Task.FromResult(DatabaseOperationResult.Ok());
            }
            catch
            {
                return Task.FromResult(DatabaseOperationResult.Fail());
            }
        }
    }
}