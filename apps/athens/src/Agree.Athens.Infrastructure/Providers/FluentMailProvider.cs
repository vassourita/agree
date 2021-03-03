using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Athens.Domain.Interfaces.Providers;
using FluentEmail.Core;
using FluentEmail.Razor;

namespace Agree.Athens.Infrastructure.Providers
{
    public class FluentMailProvider : IMailProvider
    {
        private readonly IFluentEmailFactory _fluentEmailFactory;
        public FluentMailProvider(IFluentEmailFactory fluentEmailFactory)
        {
            _fluentEmailFactory = fluentEmailFactory;

        }

        public async Task SendMailAsync(MailMessage message)
        {
            Email.DefaultRenderer = new RazorRenderer();

            await _fluentEmailFactory.Create()
                .SetFrom(message.From.Address, message.From.User)
                .To(message.To.ToString())
                .Subject(message.Subject)
                .Body(message.Body, message.IsBodyHtml)
                .SendAsync();
        }
    }
}