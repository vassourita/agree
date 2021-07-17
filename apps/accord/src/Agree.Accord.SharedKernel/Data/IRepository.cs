using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agree.Accord.SharedKernel.Data
{
    public interface IRepository<T>
        where T : Entity
    {
        Task<T> GetFirstAsync(Specification<T> specification);
        Task<IEnumerable<T>> GetAllAsync(Specification<T> specification);
        Task<IResult> InsertAsync(T entity);
        Task<IResult> UpdateAsync(T entity);
        Task<IResult> DeleteAsync(T entity);
    }
}