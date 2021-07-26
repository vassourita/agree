using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Agree.Accord.Presentation.Social.Hubs
{
    /// <summary>
    /// A SignalR Hub that manages friendship events between users.
    /// </summary>
    [Authorize]
    public class FriendshipHub : CustomHubBase
    {
        public FriendshipHub(AccountService accountService) : base(accountService)
        {
        }

        public const string FriendshipRequestReceivedMessage = "friendship_request_received";
        public const string FriendshipRequestAcceptedMessage = "friendship_request_accepted";
        public const string FriendshipRequestDeclinedMessage = "friendship_request_declined";
    }
}