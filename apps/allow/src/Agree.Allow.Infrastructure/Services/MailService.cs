using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Allow.Domain.Services;
using Agree.Allow.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Agree.Allow.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpClient _client;

        public MailService(IOptions<MailConfiguration> mailConfiguration)
        {
            _client = new SmtpClient(mailConfiguration.Value.Host, mailConfiguration.Value.Port)
            {
                Credentials = new NetworkCredential(mailConfiguration.Value.UserName, mailConfiguration.Value.Password),
                EnableSsl = true
            };
        }

        public Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            var message = new MailMessage("noreply@agree.com", to, subject, body)
            {
                IsBodyHtml = isBodyHtml
            };
            _client.SendAsync(message, null);

            return Task.CompletedTask;
        }
    }
}