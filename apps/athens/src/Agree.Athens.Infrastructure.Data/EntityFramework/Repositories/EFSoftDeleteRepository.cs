using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Repositories
{
    public class EFSoftDeleteRepository<T> : EFRepository<T>, ISoftDeleteRepository<T>
        where T : Entity, IAggregateRoot, ISoftDeletable
    {
        public EFSoftDeleteRepository(DataContext ctx) : base(ctx)
        { }

        public async Task<T> SoftDeleteAsync(Guid id)
        {
            var result = await GetByIdAsync(id);

            result.UpdatedAt = DateTime.UtcNow;
            result.DeletedAt = DateTime.UtcNow;

            return result;
        }

        public async Task<T> SoftDeleteAsync(T entity)
        {
            var result = await GetByIdAsync(entity.Id);

            entity.UpdatedAt = DateTime.UtcNow;
            entity.DeletedAt = DateTime.UtcNow;

            _ctx.Entry(result).CurrentValues.SetValues(entity);

            return entity;
        }
    }
}