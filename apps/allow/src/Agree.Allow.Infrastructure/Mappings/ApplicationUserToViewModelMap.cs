using Agree.Allow.Domain.Security;
using AutoMapper;

namespace Agree.Allow.Infrastructure.Mappings
{
    public class ApplicationUserToViewModelMap<TViewModel> : Profile
    {
        public ApplicationUserToViewModelMap()
        {
            CreateMap<ApplicationUser, TViewModel>();
        }
    }
}