using System;
using System.Threading.Tasks;
using Agree.Allow.Domain.Dtos;
using Agree.Allow.Domain.Security;

namespace Agree.Allow.Domain.Services
{
    public interface IAccountService
    {
        Task<UserAccount> FindByEmailAsync(string email);
        Task<UserAccount> FindByIdAsync(Guid id);
        Task<UserAccount> Create(RegisterDto registerInfo);
        Task<UserAccount> Update(UserAccount user, UpdateAccountDto updateAccountInfo);
        Task SendConfirmationEmail(UserAccount user, string confirmationUrl);
        Task SendWelcomeEmail(UserAccount user);
        Task ConfirmEmail(UserAccount user, string confirmationToken);
    }
}