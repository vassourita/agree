using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Aggregates.Messages;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Infrastructure.Data.EntityFramework.DataModels;
using AutoMapper;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Migrations
{
    public class DbModelToDomainEntityMap : Profile
    {
        public DbModelToDomainEntityMap()
        {
            CreateMap<UserDbModel, UserAccount>()
                .ReverseMap();

            CreateMap<UserDbModel, Author>()
                .ForMember(
                    author => author.Active,
                    config => config.MapFrom(dbModel => dbModel.DeletedAt != null)
                ).ReverseMap();

            CreateMap<UserDbModel, User>()
                .ForMember(
                    user => user.Active,
                    config => config.MapFrom(dbModel => dbModel.DeletedAt != null)
                ).ReverseMap();

            CreateMap<RoleDbModel, Role>()
                .ReverseMap();

            CreateMap<ServerDbModel, Server>()
                .ReverseMap();

            CreateMap<MessageDbModel, Message>()
                .ReverseMap();

            CreateMap<TextChannelDbModel, TextChannel>()
                .ReverseMap();
        }
    }
}