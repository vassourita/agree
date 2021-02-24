using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.DataModels;
using Agree.Athens.SharedKernel;
using Agree.Athens.SharedKernel.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Repositories
{
    public abstract class EFBaseRepository<TEntity, TModel> : IGenericRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
        where TModel : DbModel
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TModel> _dataSet;
        protected readonly IMapper _mapper;

        public EFBaseRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _dataSet = context.Set<TModel>();
            _mapper = mapper;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var item = _mapper.Map<TModel>(entity);

            await _dataSet.AddAsync(item);

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            var item = _mapper.Map<TModel>(entity);

            _dataSet.Remove(item);
        }

        public Task DeleteAsync(TEntity entity)
        {
            var item = _mapper.Map<TModel>(entity);

            _dataSet.Remove(item);

            return Task.CompletedTask;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var item = await _dataSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
            var entity = _mapper.Map<TEntity>(item);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var dbItem = await GetByIdAsync(entity.Id);

            var item = _mapper.Map<TModel>(entity);

            _context.Entry(dbItem).CurrentValues.SetValues(item);

            return entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}