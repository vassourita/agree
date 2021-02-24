using System.Net.Mail;
using System.Threading.Tasks;

namespace Agree.Athens.Domain.Interfaces.Providers
{
    public interface IMailProvider
    {
        Task SendMailAsync(MailMessage message);
    }
}