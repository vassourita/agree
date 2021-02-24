using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;

namespace Agree.Athens.Domain.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAccountConfirmationMailAsync(UserAccount newAccount, string confirmationUrl);
        Task SendChangePasswordMailAsync(UserAccount account, string changePasswordUrl);
    }
}