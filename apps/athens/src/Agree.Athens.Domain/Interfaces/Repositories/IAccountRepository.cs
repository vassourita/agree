using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;

namespace Agree.Athens.Domain.Interfaces.Repositories
{
    public interface IAccountRepository : ISoftDeleteRepository<UserAccount>
    {
        Task<bool> EmailIsInUseAsync(string email);
        Task<UserAccount> GetByEmailAsync(string email);
        Task<bool> TagIsInUseAsync(UserTag tag, string userName);
    }
}