using System.Collections.Generic;
using System.Threading.Tasks;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> SearchAsync(string query);
    }
}