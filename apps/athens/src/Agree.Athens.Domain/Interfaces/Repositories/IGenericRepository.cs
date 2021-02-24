using System;
using System.Threading.Tasks;
using Agree.Athens.SharedKernel;
using Agree.Athens.SharedKernel.Data;

namespace Agree.Athens.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> : IRepository<T>
        where T : Entity, IAggregateRoot
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(T entity);
    }
}