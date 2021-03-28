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
using Agree.Athens.Domain.Dtos;
using Agree.Athens.Domain.Aggregates.Account;

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

        public async Task<IEnumerable<Server>> Search(SearchServerDto searchServerDto, UserAccount searchedBy)
        {
            var query = _dataSet
                .Where(server =>
                    EF.Functions.ILike(server.Name, $"%{searchServerDto.Query}%")
                    || EF.Functions.ILike(server.Description, $"%{searchServerDto.Query}%")
                    || EF.Functions.ILike(server.Id.ToString(), $"%{searchServerDto.Query}%")
                )
                .Where(server => !server.Users.Select(u => u.Id).Contains(searchedBy.Id)
                                 ? server.Privacy != ServerPrivacy.Private
                                 : true)
                .Skip((searchServerDto.Page - 1) * searchServerDto.PageLimit)
                .Take(searchServerDto.PageLimit);


            query = searchServerDto.OrderBy.ToLower() switch
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