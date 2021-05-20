using System;
using System.Linq;
using System.Threading.Tasks;
using Agree.Allow.Domain.Services;
using Agree.Allow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Agree.Allow.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _ctx;
        public TagService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> GenerateTag(string userName)
        {
            var rnd = new Random();
            var tag = rnd.Next(1, 9999);

            var tagsInUse = await _ctx.Users.Where(u => u.DisplayName == userName).Select(u => u.Tag).ToArrayAsync();

            while (tagsInUse.Contains(tag))
            {
                tag = rnd.Next(1, 9999);
            }

            return tag;
        }
    }
}