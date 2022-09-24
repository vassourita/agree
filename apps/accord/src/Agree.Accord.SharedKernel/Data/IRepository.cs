namespace Agree.Accord.SharedKernel.Data;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository<T> : IUnitOfWork
    where T : class
{
    Task<T> GetFirstAsync(Specification<T> specification);
    Task<IEnumerable<T>> GetAllAsync(Specification<T> specification);
    Task<IResult> InsertAsync(T entity);
    Task<IResult> UpdateAsync(T entity);
    Task<IResult> DeleteAsync(T entity);
}