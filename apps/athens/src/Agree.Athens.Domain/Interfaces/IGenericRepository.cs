using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Agree.Athens.SharedKernel;
using Agree.Athens.SharedKernel.Data;
using Agree.Athens.SharedKernel.Specification;

namespace Agree.Athens.Domain.Interfaces
{
    public interface IGenericRepository<T> : IRepository<T>
        where T : Entity, IAggregateRoot
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetBySpecificationAsync(Specification<T> specification);
        Task<IEnumerable<T>> ListAsync(Specification<T> specification);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(T entity);
    }
}