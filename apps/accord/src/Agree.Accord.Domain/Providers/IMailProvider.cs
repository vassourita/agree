using System.Threading.Tasks;

namespace Agree.Accord.Domain.Providers
{
    /// <summary>
    /// The interface for a mail sender.
    /// </summary>
    public interface IMailProvider
    {
        /// <summary>
        /// Send a email message asynchronously.
        /// </summary>
        /// <param name="to">The recipient of the message.</param>
        /// <param name="subject">The subject of the message.</param>
        /// <param name="body">The body of the message.</param>
        /// <param name="isBodyHtml">A flag indicating whether the body is HTML. Defaults to true.</param>
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
    }
}