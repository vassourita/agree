namespace Agree.Accord.Presentation.Social.Hubs;

using Agree.Accord.Domain.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

/// <summary>
/// A SignalR Hub that manages friendship events between users.
/// </summary>
[Authorize]
public class FriendshipHub : CustomHubBase
{
    public const string FriendshipRequestReceivedMessage = "friendship_request_received";
    public const string FriendshipRequestAcceptedMessage = "friendship_request_accepted";
    public const string FriendshipRequestDeclinedMessage = "friendship_request_declined";
    public const string FriendshipRemovedMessage = "friendship_removed";

    public FriendshipHub(AccountService accountService, ILogger<CustomHubBase> logger) : base(accountService, logger)
    {
    }
}