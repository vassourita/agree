using System.Collections.Generic;
using System.Threading.Tasks;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity
{
    /// <summary>
    /// The interface for a repository of users accounts. Has all the methods from a generic IRepository, plus a search method.
    /// </summary>
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        /// <summary>
        /// Searches for users by their nameTag.
        /// </summary>
        Task<IEnumerable<ApplicationUser>> SearchAsync(string query, IPagination pagination);
    }
}