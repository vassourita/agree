using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Interfaces.Providers;

namespace Agree.Athens.Application.Services
{
    public class MailService
    {
        private readonly IMailProvider _mailProvider;

        public MailService(IMailProvider mailProvider)
        {
            _mailProvider = mailProvider;
        }

        public async Task SendAccountConfirmationMailAsync(UserAccount newAccount, string confirmationUrl)
        {
        }

        public Task SendChangePasswordMailAsync(UserAccount account, string changePasswordUrl)
        {
            throw new System.NotImplementedException();
        }
    }
}