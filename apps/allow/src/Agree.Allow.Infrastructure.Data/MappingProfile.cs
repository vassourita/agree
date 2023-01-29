namespace Agree.Allow.Infrastructure.Data;

using Agree.Allow.Domain;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserAccount, UserAccountDbModel>()
            .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag.Value));
        CreateMap<UserAccountDbModel, UserAccount>()
            .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => DiscriminatorTag.Parse(src.Tag)));
    }
}