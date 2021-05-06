using System;
using System.Linq;
using System.Threading.Tasks;
using Agree.Allow.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Agree.Allow.Services
{
    public class TagService
    {
        private readonly ApplicationContext _ctx;
        public TagService(ApplicationContext ctx)
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