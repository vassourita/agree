using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agree.Athens.Domain.Entities;
using Agree.Athens.Domain.Interfaces.Common;

namespace Agree.Athens.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T>
        where T : BaseEntity, IAggregateRoot
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IList<T>> ListAsync();
        Task<IList<T>> ListAsync(ISpecification<T> specification);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(Guid id);
        Task SoftDeleteAsync(T entity);
        Task SoftDeleteAsync(Guid id);
    }
}