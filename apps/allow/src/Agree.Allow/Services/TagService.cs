using System.Threading.Tasks;
using Agree.Allow.Data.Contexts;

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
            return 0;
        }
    }
}