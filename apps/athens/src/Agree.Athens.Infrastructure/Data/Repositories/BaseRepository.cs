using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Agree.Athens.Domain.Entities;
using Agree.Athens.Domain.Interfaces;
using Agree.Athens.Domain.Interfaces.Common;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Data.Contexts;
using System;
using Agree.Athens.Domain.Exceptions;

namespace Agree.Athens.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity, IAggregateRoot
    {
        protected readonly DataContext _context;
        protected readonly DbSet<T> _dataSet;

        public BaseRepository(DataContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var item = await _dataSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
            if (item == null)
            {
                throw new EntityNotFoundException<T>(id);
            }
            return item;
        }

        public async Task<IList<T>> ListAsync()
        {
            var items = await _dataSet
                .AsNoTracking()
                .ToListAsync();
            if (items == null || items.Count < 1)
            {
                throw new EntityNotFoundException<T>();
            }
            return items;
        }

        public async Task<IList<T>> ListAsync(ISpecification<T> specification)
        {
            return await _dataSet
                .Where(item => specification.IsSatisfied(item))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;

            await _dataSet.AddAsync(entity);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result = await _dataSet.SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));
            if (result == null)
            {
                throw new EntityNotFoundException<T>(entity.Id);
            }

            entity.UpdatedAt = DateTime.UtcNow;

            _context.Entry(result).CurrentValues.SetValues(entity);

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            var result = await _dataSet.FindAsync(entity);
            if (result == null)
            {
                throw new EntityNotFoundException<T>(entity.Id);
            }

            _dataSet.Remove(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var result = await _dataSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
            if (result == null)
            {
                throw new EntityNotFoundException<T>(id);
            }

            _dataSet.Remove(result);
        }
    }
}