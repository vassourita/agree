using Agree.Allow.Domain.Security;
using Agree.Allow.Presentation.ViewModels;
using AutoMapper;

namespace Agree.Allow.Presentation.Mappings
{
    public class EntityToViewModelMap : Profile
    {
        public EntityToViewModelMap()
        {
            CreateMap<UserAccount, UserAccountViewModel>()
                .ForMember(viewModel => viewModel.UserName, memberOptions =>
                {
                    memberOptions.MapFrom(entity => entity.DisplayName);
                })
                .ForMember(viewModel => viewModel.Tag, memberOptions =>
                {
                    memberOptions.MapFrom(entity => entity.Tag.ToString().PadLeft(4, '0'));
                })
                .ForMember(viewModel => viewModel.Verified, memberOptions =>
                {
                    memberOptions.MapFrom(entity => entity.EmailConfirmed);
                });
        }
    }
}