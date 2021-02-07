using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Entities;

namespace Agree.Athens.Domain.Interfaces.Repositories
{
    public interface ISoftDeleteRepository<T>
        where T : BaseEntity, IAggregateRoot, ISoftDeletable
    {
        Task SoftDeleteAsync(T entity);
        Task SoftDeleteAsync(Guid id);
    }
}