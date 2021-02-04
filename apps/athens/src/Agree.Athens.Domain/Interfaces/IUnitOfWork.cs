using System.Threading.Tasks;

namespace Agree.Athens.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
        Task Rollback();
    }
}