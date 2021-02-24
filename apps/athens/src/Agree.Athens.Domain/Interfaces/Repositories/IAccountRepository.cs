using Agree.Athens.Domain.Aggregates.Account;

namespace Agree.Athens.Domain.Interfaces.Repositories
{
    public interface IAccountRepository : ISoftDeleteRepository<UserAccount>
    {

    }
}