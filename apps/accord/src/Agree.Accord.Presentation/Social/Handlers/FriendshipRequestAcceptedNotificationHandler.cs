namespace Agree.Accord.Domain.Social.Notifications;

using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Presentation;
using Agree.Accord.Presentation.Shared;
using Agree.Accord.Presentation.Social.ViewModels;
using MediatR;
using Microsoft.AspNetCore.SignalR;

public class FriendshipRequestAcceptedNotificationHandler : INotificationHandler<FriendshipRequestAcceptedNotification>
{
    private readonly IHubContext<SocketHub> _hubContext;

    public FriendshipRequestAcceptedNotificationHandler(IHubContext<SocketHub> hubContext)
        => _hubContext = hubContext;

    public async Task Handle(FriendshipRequestAcceptedNotification notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients
            .User(notification.Friendship.FromId.ToString())
            .SendAsync(
                HubEvents.FriendshipRequestAccepted,
                FriendshipRequestViewModel.FromEntity(notification.Friendship)
            );
    }
}