using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

namespace Agree.Accord.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public Task CommitAsync()
        {
            return SaveChangesAsync();
        }
    }
}