using System.Linq;
using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using Agree.Athens.SharedKernel.Data;

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

        public async Task<IEnumerable<Server>> Search(string search, string orderBy, Paginated paginated)
        {
            var query = _dataSet
                .Where(server =>
                    EF.Functions.ILike(server.Name, $"%{search}%")
                    || EF.Functions.ILike(server.Description, $"%{search}%")
                    || EF.Functions.ILike(server.Id.ToString(), $"%{search}%")
                )
                .Where(server => server.Privacy != ServerPrivacy.Private)
                .Skip((paginated.Page - 1) * paginated.PageLimit)
                .Take(paginated.PageLimit);


            query = orderBy.ToLower() switch
            {
                "popular" => query.OrderBy(server => server.Users.Count()),
                "creationdate" => query.OrderBy(server => server.CreatedAt),
                "creationdate_desc" => query.OrderByDescending(server => server.CreatedAt),
                _ => query.OrderBy(server => server.Users.Count())
            };

            var results = await query.AsNoTracking().ToListAsync();

            return _mapper.Map<IEnumerable<Server>>(results);
        }
    }
}