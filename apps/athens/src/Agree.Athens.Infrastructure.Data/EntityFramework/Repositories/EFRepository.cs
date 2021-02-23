using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Athens.Domain.Interfaces;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.SharedKernel;
using Agree.Athens.SharedKernel.Data;
using Agree.Athens.SharedKernel.Specification;
using Microsoft.EntityFrameworkCore;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Repositories
{
    public class EFRepository<T> : IGenericRepository<T>
        where T : Entity, IAggregateRoot
    {
        public EFRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        protected readonly DataContext _ctx;
        protected readonly DbSet<T> _dataSet;

        public IUnitOfWork UnitOfWork => _ctx;

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _dataSet.AddAsync(entity);

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var result = await GetByIdAsync(id);

            _dataSet.Remove(result);
        }

        public async Task DeleteAsync(T entity)
        {
            var result = await GetByIdAsync(entity.Id);

            _dataSet.Remove(result);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var item = await _dataSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return item;
        }

        public async Task<T> GetBySpecificationAsync(Specification<T> specification)
        {
            var item = await _dataSet.SingleOrDefaultAsync(specification.ToExpression());
            return item;
        }

        public async Task<IEnumerable<T>> ListAsync(Specification<T> specification)
        {
            var items = await _dataSet
                .Where(specification.ToExpression())
                .AsNoTracking()
                .ToListAsync();
            return items;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result = await GetByIdAsync(entity.Id);

            entity.UpdatedAt = DateTime.UtcNow;

            _ctx.Entry(result).CurrentValues.SetValues(entity);

            return entity;
        }

        private bool _disposedValue { get; set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}