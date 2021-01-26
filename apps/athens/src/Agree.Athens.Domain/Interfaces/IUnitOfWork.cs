using System.Threading.Tasks;

namespace Agree.Athens.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}