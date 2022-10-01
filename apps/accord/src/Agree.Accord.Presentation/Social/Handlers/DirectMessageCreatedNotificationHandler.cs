namespace Agree.Accord.Domain.Social.Notifications;

using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Presentation;
using Agree.Accord.Presentation.Shared;
using Agree.Accord.Presentation.Social.ViewModels;
using MediatR;
using Microsoft.AspNetCore.SignalR;

public class DirectMessageCreatedNotificationHandler : INotificationHandler<DirectMessageCreatedNotification>
{
    private readonly IHubContext<SocketHub> _hubContext;

    public DirectMessageCreatedNotificationHandler(IHubContext<SocketHub> hubContext)
        => _hubContext = hubContext;

    public async Task Handle(DirectMessageCreatedNotification notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients
            .User(notification.DirectMessage.To.Id.ToString())
            .SendAsync(
                HubEvents.DirectMessageCreated,
                DirectMessageViewModel.FromEntity(notification.DirectMessage)
            );
    }
}