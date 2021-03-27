using System.Linq;
using System.Reflection;
using Agree.Athens.Application.ViewModels;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Aggregates.Messages;
using Agree.Athens.Domain.Aggregates.Servers;
using AutoMapper;

namespace Agree.Athens.Application.Mappings
{
    public class DomainEntityToViewModelProfile : Profile
    {
        public DomainEntityToViewModelProfile()
        {
            CreateMap<UserAccount, AccountViewModel>()
                .ForMember(model => model.Tag, config => config.MapFrom(entity => entity.Tag.ToString()));
            CreateMap<UserAccount, ServerMemberViewModel>()
                .ForMember(model => model.Tag, config => config.MapFrom(entity => entity.Tag.ToString()));

            CreateMap<User, AccountViewModel>()
                .ForMember(model => model.Tag, config => config.MapFrom(entity => entity.Tag.ToString()));
            CreateMap<User, ServerMemberViewModel>()
                .ForMember(model => model.Tag, config => config.MapFrom(entity => entity.Tag.ToString()))
                .ForMember(model => model.Roles, config => config.MapFrom(entity => entity.Roles.Select(r => new ServerMemberViewModel.Role(r.Id))));

            CreateMap<Server, ServerViewModel>()
                .ForMember(model => model.Members, config => config.MapFrom(entity => entity.Users));

            CreateMap<Category, CategoryViewModel>();

            CreateMap<Message, MessageViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(entity => entity.Author));

            CreateMap<Role, RoleViewModel>()
                .ForMember(model => model.ColorHex, config => config.MapFrom(entity => entity.ColorHex.ToString()))
                .ForMember(model => model.Permissions, config => config.MapFrom(entity => new RolePermissions
                {
                    CanAddUsers = entity.CanAddUsers,
                    CanDeleteServer = entity.CanDeleteServer,
                    CanRemoveUsers = entity.CanRemoveUsers,
                    CanUpdateServerName = entity.CanUpdateServerName,
                }));

            CreateMap<TextChannel, TextChannelViewModel>();
        }
    }
}