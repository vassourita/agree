using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Aggregates.Messages;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Infrastructure.Data.EntityFramework.DataModels;
using AutoMapper;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Mappings
{
    public class DbModelToDomainEntityProfile : Profile
    {
        public DbModelToDomainEntityProfile()
        {
            CreateMap<UserDbModel, UserAccount>()
                .ReverseMap();

            CreateMap<UserDbModel, User>()
                .ForMember(
                    user => user.Active,
                    config => config.MapFrom(dbModel => dbModel.DeletedAt != null)
                ).ReverseMap();

            CreateMap<RoleDbModel, Role>()
                .ReverseMap();

            CreateMap<ServerDbModel, Server>()
                .ReverseMap();

            CreateMap<CategoryDbModel, Category>()
                .ReverseMap();

            CreateMap<MessageDbModel, Message>()
                .ReverseMap();

            CreateMap<TextChannelDbModel, TextChannel>()
                .ReverseMap();
        }
    }
}