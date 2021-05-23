using System;
using System.Threading.Tasks;
using Agree.Allow.Domain.Dtos;
using Agree.Allow.Domain.Security;

namespace Agree.Allow.Domain.Services
{
    public interface IAccountService
    {
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(Guid id);
        Task<ApplicationUser> Create(RegisterDto registerInfo);
        Task<ApplicationUser> Update(ApplicationUser user, UpdateAccountDto updateAccountInfo);
        Task SendConfirmationEmail(ApplicationUser user, string confirmationUrl);
        Task SendWelcomeEmail(ApplicationUser user);
        Task ConfirmEmail(ApplicationUser user, string confirmationToken);
    }
}