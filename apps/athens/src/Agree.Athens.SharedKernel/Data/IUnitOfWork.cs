using System.Threading.Tasks;

namespace Agree.Athens.SharedKernel.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task<bool> Rollback();
    }
}