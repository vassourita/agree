using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Entities;
using Agree.Athens.Domain.Interfaces;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Repositories
{
    public class SoftDeleteRepository<T> : BaseRepository<T>, ISoftDeleteRepository<T>
        where T : BaseEntity, IAggregateRoot, ISoftDeletable
    {
        public SoftDeleteRepository(DataContext context) : base(context)
        {
        }

        public async Task SoftDeleteAsync(T entity)
        {
            entity = await GetByIdAsync(entity.Id);
            if (entity.DeletedAt != null)
            {
                entity.DeletedAt = DateTime.UtcNow;
            }
            await UpdateAsync(entity);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity.DeletedAt != null)
            {
                entity.DeletedAt = DateTime.UtcNow;
            }
            await UpdateAsync(entity);
        }
    }
}