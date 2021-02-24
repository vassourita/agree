using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Infrastructure.Configuration;

namespace Agree.Athens.Infrastructure.Providers
{
    public class NativeMailProvider : IMailProvider
    {
        private readonly SmtpClient client;

        public NativeMailProvider(MailConfiguration mailConfiguration)
        {
            client = new SmtpClient(mailConfiguration.Host, mailConfiguration.Port)
            {
                Credentials = new NetworkCredential(mailConfiguration.Username, mailConfiguration.Password),
                EnableSsl = mailConfiguration.EnableSsl
            };
        }

        public Task SendMailAsync(MailMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}