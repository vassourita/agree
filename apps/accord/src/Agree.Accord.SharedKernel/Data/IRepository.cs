using System.Collections.Generic;
using System.Threading.Tasks;
namespace Agree.Accord.SharedKernel.Data
{
    public interface IRepository<T>
        where T : Entity
    {
        Task<T> GetFirstAsync();
        Task<T> GetFirstAsync(Specification<T> specification);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Specification<T> specification);
        Task<Result> InsertAsync(T entity);
        Task<Result> UpdateAsync(T entity);
        Task<Result> DeleteAsync(T entity);
    }
}