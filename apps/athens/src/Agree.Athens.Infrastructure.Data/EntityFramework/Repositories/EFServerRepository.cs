using System.Linq;
using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Repositories
{
    public class EFServerRepository : EFBaseRepository<Server, ServerDbModel>, IServerRepository
    {
        public EFServerRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async new Task<Server> AddAsync(Server server)
        {
            var model = _mapper.Map<ServerDbModel>(server);

            _context.Entry(model.Users.First()).State = EntityState.Unchanged;

            await _context.Servers.AddAsync(model);

            return server;
        }
    }
}