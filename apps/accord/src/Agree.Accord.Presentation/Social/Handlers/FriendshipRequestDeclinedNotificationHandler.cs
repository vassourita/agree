namespace Agree.Accord.Domain.Social.Notifications;

using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Presentation;
using Agree.Accord.Presentation.Shared;
using Agree.Accord.Presentation.Social.ViewModels;
using MediatR;
using Microsoft.AspNetCore.SignalR;

public class FriendshipRequestDeclinedNotificationHandler : INotificationHandler<FriendshipRequestDeclinedNotification>
{
    private readonly IHubContext<SocketHub> _hubContext;

    public FriendshipRequestDeclinedNotificationHandler(IHubContext<SocketHub> hubContext)
        => _hubContext = hubContext;

    public async Task Handle(FriendshipRequestDeclinedNotification notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients
            .User(notification.Friendship.FromId.ToString())
            .SendAsync(
                HubEvents.FriendshipRequestDeclined,
                FriendshipRequestViewModel.FromEntity(notification.Friendship)
            );
    }
}