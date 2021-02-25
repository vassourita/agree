using System.IO;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Domain.Interfaces.Services;

namespace Agree.Athens.Application.Services
{
    public class MailService : IMailService
    {
        private readonly IMailProvider _mailProvider;

        public MailService(IMailProvider mailProvider)
        {
            _mailProvider = mailProvider;
        }

        public async Task SendAccountConfirmationMailAsync(UserAccount newAccount, string confirmationUrl)
        {
            var dict = new Dictionary<string, string>
            {
                ["Tag"] = newAccount.Tag.Value,
                ["UserName"] = newAccount.UserName,
                ["ConfirmationUrl"] = "",
            };
            var templatePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Agree.Athens.Application", "Views", "Mail", "ConfirmationMail.html");

            var body =
                $@"<html><body><p>Welcome to Agree, {newAccount.UserName}! <a href='{confirmationUrl}'>Click here</a> to confirm your account</p></body></html>";

            var message = new MailMessage("noreply@agree.com.br", newAccount.Email)
            {
                IsBodyHtml = true,
                Body = body
            };
            await _mailProvider.SendMailAsync(message);
        }

        public Task SendChangePasswordMailAsync(UserAccount account, string changePasswordUrl)
        {
            throw new System.NotImplementedException();
        }
    }
}