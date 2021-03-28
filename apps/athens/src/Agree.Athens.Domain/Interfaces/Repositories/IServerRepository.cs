using System.Collections.Generic;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Domain.Dtos;
using Agree.Athens.SharedKernel.Data;

namespace Agree.Athens.Domain.Interfaces.Repositories
{
    public interface IServerRepository : IGenericRepository<Server>
    {
        Task<IEnumerable<Server>> Search(SearchServerDto searchServerDto);
    }
}