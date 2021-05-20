using System.Threading.Tasks;

namespace Agree.Allow.Domain.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
    }
}