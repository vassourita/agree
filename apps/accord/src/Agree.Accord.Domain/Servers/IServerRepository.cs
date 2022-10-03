namespace Agree.Accord.Domain.Servers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agree.Accord.SharedKernel.Data;

public interface IServerRepository : IRepository<Server, Guid>
{
    public Task<IEnumerable<Server>> SearchAsync(string query, Pagination pagination);
}
