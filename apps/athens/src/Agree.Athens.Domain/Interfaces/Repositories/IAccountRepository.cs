using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;

namespace Agree.Athens.Domain.Interfaces.Repositories
{
    public interface IAccountRepository : ISoftDeleteRepository<UserAccount>
    {
        Task<bool> EmailIsInUse(string email);
        Task<bool> TagIsInUse(UserTag tag, string userName);
    }
}