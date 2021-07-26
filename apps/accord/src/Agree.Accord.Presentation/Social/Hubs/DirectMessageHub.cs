using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Agree.Accord.Presentation.Social.Hubs
{
    /// <summary>
    /// A SignalR Hub that manages friendship events between users.
    /// </summary>
    [Authorize]
    public class DirectMessageHub : CustomHubBase
    {
        public const string DirectMessageReceivedMessage = "direct_message_received";

        public DirectMessageHub(AccountService accountService, ILogger<CustomHubBase> logger) : base(accountService, logger)
        {
        }
    }
}