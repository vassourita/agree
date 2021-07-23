using System.Threading.Tasks;

namespace Agree.Accord.Domain.Providers
{
    public interface IMailProvider
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
    }
}