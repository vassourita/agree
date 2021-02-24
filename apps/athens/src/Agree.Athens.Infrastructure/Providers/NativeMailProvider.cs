using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Agree.Athens.Infrastructure.Providers
{
    public class NativeMailProvider : IMailProvider
    {
        private readonly SmtpClient _client;

        public NativeMailProvider(IOptions<MailConfiguration> mailConfiguration)
        {
            _client = new SmtpClient(mailConfiguration.Value.Host, mailConfiguration.Value.Port)
            {
                Credentials = new NetworkCredential(mailConfiguration.Value.Username, mailConfiguration.Value.Password),
                EnableSsl = mailConfiguration.Value.EnableSsl
            };
        }

        public async Task SendMailAsync(MailMessage message)
        {
            await _client.SendMailAsync(message);
        }
    }
}