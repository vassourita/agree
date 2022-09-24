namespace Agree.Accord.Presentation.Social.Hubs;

using Agree.Accord.Domain.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

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