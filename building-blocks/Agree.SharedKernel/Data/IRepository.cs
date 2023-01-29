namespace Agree.SharedKernel.Data;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository<T, TId> : IUnitOfWork
    where T : class, IEntity<TId>
{
    Task<T?> GetFirstAsync(ISpecification<T> specification);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification);
    Task<T> InsertAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}