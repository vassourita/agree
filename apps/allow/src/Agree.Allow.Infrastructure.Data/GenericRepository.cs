namespace Agree.Allow.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Allow.Domain;
using Agree.SharedKernel;
using Agree.SharedKernel.Data;
using Agree.SharedKernel.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<TDbModel, TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TDbModel : class
{
    protected readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly SpecificationConverter<TDbModel, TEntity, TId> _specificationConverter = new();

    public GenericRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Task CommitAsync() => _dbContext.SaveChangesAsync();

    public Task DeleteAsync(TEntity entity)
    {
        try
        {
            var dbModel = _mapper.Map<TDbModel>(entity);
            _dbContext.Set<TDbModel>().Remove(dbModel);
            return Task.FromResult(DatabaseOperationResult.Ok());
        }
        catch
        {
            return Task.FromResult(DatabaseOperationResult.Fail());
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var result = await _dbContext.Set<TDbModel>().ToListAsync();
        return _mapper.Map<IEnumerable<TEntity>>(result);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Specification<TEntity> specification)
    {
        if (specification is PaginatedSpecification<TEntity> paginatedSpecification)
        {
            var convertedExpression = _specificationConverter.Convert(paginatedSpecification);
            var result = await _dbContext.Set<TDbModel>()
                .Where(convertedExpression)
                .Skip((paginatedSpecification.Pagination.Page - 1) * paginatedSpecification.Pagination.PageSize)
                .Take(paginatedSpecification.Pagination.PageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TEntity>>(result);
        }
        else
        {
            var convertedExpression = _specificationConverter.Convert(specification);
            var result = await _dbContext.Set<TDbModel>().Where(convertedExpression).ToListAsync();
            return _mapper.Map<IEnumerable<TEntity>>(result);
        }
    }

    public async Task<TEntity?> GetFirstAsync(Specification<TEntity> specification)
    {
        var convertedExpression = _specificationConverter.Convert(specification);
        var result = await _dbContext.Set<TDbModel>().Where(convertedExpression).FirstOrDefaultAsync();
        return _mapper.Map<TEntity>(result);
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        try
        {
            var dbModel = _mapper.Map<TDbModel>(entity);
            var response = await _dbContext.Set<TDbModel>().AddAsync(dbModel);
            return _mapper.Map<TEntity>(response.Entity);
        }
        catch (Exception e)
        {
            throw new RepositoryOperationException(nameof(TEntity), $"An error ocurred while inserting entity {nameof(TEntity)}#{entity.Id}", e);
        }
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            var dbModel = _mapper.Map<TDbModel>(entity);
            _dbContext.Set<TDbModel>().Update(dbModel);
            return Task.FromResult(entity);
        }
        catch (Exception e)
        {
            throw new RepositoryOperationException(nameof(TEntity), $"An error ocurred while updating entity {nameof(TEntity)}#{entity.Id}", e);
        }
    }
}