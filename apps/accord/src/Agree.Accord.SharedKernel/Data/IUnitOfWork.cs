namespace Agree.Accord.SharedKernel.Data;

using System.Threading.Tasks;

public interface IUnitOfWork
{
    Task CommitAsync();
}