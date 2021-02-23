using System;
using System.Threading.Tasks;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Interfaces.Repositories
{
    public interface ISoftDeleteRepository<T> : IGenericRepository<T>
        where T : Entity, IAggregateRoot, ISoftDeletable
    {
        Task<T> SoftDeleteAsync(Guid id);
        Task<T> SoftDeleteAsync(T entity);
    }
}