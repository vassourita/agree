namespace Agree.Accord.Infrastructure.Providers;

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Accord.Domain.Providers;
using Agree.Accord.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

/// <summary>
/// A implementation of <see cref="IMailProvider"/> using C#'s native <see cref="SmtpClient"/>.
/// </summary>
public class NativeMailProvider : IMailProvider, IDisposable
{
    private readonly SmtpClient _client;
    private const string DefaultSender = "noreply@agree.com";

    public NativeMailProvider(IOptions<NativeMailProviderOptions> options) => _client = new SmtpClient(options.Value.Host, options.Value.Port)
    {
        Credentials = new NetworkCredential(options.Value.UserName, options.Value.Password),
        EnableSsl = true
    };

    public Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        var message = new MailMessage(DefaultSender, to, subject, body)
        {
            IsBodyHtml = isBodyHtml
        };
        _client.SendAsync(message, null);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}