namespace Agree.Accord.SharedKernel.Data;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository<T, TId> : IUnitOfWork
    where T : class, IEntity<TId>
{
    Task<T> GetFirstAsync(Specification<T> specification);
    Task<IEnumerable<T>> GetAllAsync(Specification<T> specification);
    Task<T> InsertAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}