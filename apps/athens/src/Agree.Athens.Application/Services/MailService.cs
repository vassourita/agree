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
        private readonly IMailTemplateProvider _mailTemplateProvider;

        public MailService(IMailProvider mailProvider, IMailTemplateProvider mailTemplateProvider)
        {
            _mailProvider = mailProvider;
            _mailTemplateProvider = mailTemplateProvider;
        }

        public async Task SendAccountConfirmationMailAsync(UserAccount newAccount, string confirmationUrl)
        {
            var parameters = new Dictionary<string, string>();
            parameters["UserName"] = newAccount.UserName;
            parameters["Tag"] = newAccount.Tag.ToString();
            parameters["ConfirmationUrl"] = confirmationUrl;

            var body = _mailTemplateProvider.FromHtml("", parameters);
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