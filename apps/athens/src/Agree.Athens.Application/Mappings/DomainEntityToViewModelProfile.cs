using Agree.Athens.Application.ViewModels;
using Agree.Athens.Domain.Aggregates.Account;
using AutoMapper;

namespace Agree.Athens.Application.Mappings
{
    public class DomainEntityToViewModelProfile : Profile
    {
        public DomainEntityToViewModelProfile()
        {
            CreateMap<UserAccount, AccountViewModel>()
                .ForMember(model => model.Tag, config => config.MapFrom(entity => entity.Tag.ToString()));
        }
    }
}