using System.Threading.Tasks;

namespace Agree.Accord.SharedKernel.Data
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}