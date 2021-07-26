using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Accord.Domain.Providers;
using Agree.Accord.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Agree.Accord.Infrastructure.Providers
{
    /// <summary>
    /// A implementation of <see cref="IMailProvider"/> using C#'s native <see cref="SmtpClient"/>.
    /// </summary>
    public class NativeMailProvider : IMailProvider
    {
        private readonly SmtpClient _client;

        public NativeMailProvider(IOptions<NativeMailProviderOptions> options)
        {
            _client = new SmtpClient(options.Value.Host, options.Value.Port)
            {
                Credentials = new NetworkCredential(options.Value.UserName, options.Value.Password),
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