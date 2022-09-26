namespace Agree.Accord.Domain.Social.Notifications;

using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Presentation;
using Agree.Accord.Presentation.Shared;
using Agree.Accord.Presentation.Social.ViewModels;
using MediatR;
using Microsoft.AspNetCore.SignalR;

public class FriendshipRequestCreatedNotificationHandler : INotificationHandler<FriendshipRequestCreatedNotification>
{
    private readonly IHubContext<SocketHub> _hubContext;

    public FriendshipRequestCreatedNotificationHandler(IHubContext<SocketHub> hubContext)
        => _hubContext = hubContext;

    public async Task Handle(FriendshipRequestCreatedNotification notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients
            .User(notification.Friendship.ToId.ToString())
            .SendAsync(
                HubEvents.FriendshipRequestCreated,
                FriendshipRequestViewModel.FromEntity(notification.Friendship)
            );
    }
}