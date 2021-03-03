using System;
using System.Threading.Tasks;
using Agree.Athens.Application.Security;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Repositories
{

    public class EFTokenRepository : ITokenRepository
    {
        protected readonly DataContext _context;
        protected readonly DbSet<RefreshToken> _dataSet;

        public EFTokenRepository(DataContext context)
        {
            _context = context;
            _dataSet = context.Set<RefreshToken>();
        }

        public async Task<RefreshToken> AddAsync(RefreshToken token)
        {
            await _dataSet.AddAsync(token);
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<RefreshToken> GetAsync(string tokenValue)
        {
            var token = await _dataSet.FirstOrDefaultAsync(t => t.Token == tokenValue);
            return token;
        }
    }
}