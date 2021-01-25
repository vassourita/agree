using System.Collections.Generic;
using System.Threading.Tasks;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces.Common;

namespace Agree.Athens.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T, TId>
        where T : BaseEntity<TId>, IAggregateRoot
    {
        Task<T> GetByIdAsync(TId id);
        Task<IList<T>> ListAsync();
        Task<IList<T>> ListAsync(ISpecification<T> specification);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}