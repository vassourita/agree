using System.Threading.Tasks;
using Agree.Athens.Domain.Interfaces;
using Agree.Athens.Infrastructure.Data.Contexts;

namespace Agree.Athens.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}