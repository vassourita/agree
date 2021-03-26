using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.DataModels;
using AutoMapper;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Repositories
{
    public class EFServerRepository : EFBaseRepository<Server, ServerDbModel>, IServerRepository
    {
        public EFServerRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}