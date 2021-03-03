using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Athens.Application.Views.Mail;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Interfaces.Providers;

namespace Agree.Athens.Application.Services
{
    public class MailService
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
            var templatePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "..",
                "Agree.Athens.Application",
                "Views",
                "Mail",
                "ConfirmAccount.cshtml"
            );

            var template = string.Empty;
            using (var sr = new StreamReader(templatePath))
            {
                template = sr.ReadToEnd();
            }

            var body = await _mailTemplateProvider.CompileAsync(template, new ConfirmAccountMailModel
            {
                ConfirmationUrl = confirmationUrl,
                Tag = newAccount.Tag.ToString(),
                UserName = newAccount.UserName
            });

            var message = new MailMessage("noreply@agree.com.br", newAccount.Email)
            {
                IsBodyHtml = true,
                Body = body,
                Subject = "Bem-vindx ao Agree! :3"
            };
            await _mailProvider.SendMailAsync(message);
        }

        public Task SendChangePasswordMailAsync(UserAccount account, string changePasswordUrl)
        {
            throw new System.NotImplementedException();
        }
    }
}