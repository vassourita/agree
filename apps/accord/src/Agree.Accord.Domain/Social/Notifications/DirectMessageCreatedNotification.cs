namespace Agree.Accord.Domain.Social.Notifications;

using MediatR;

public class DirectMessageCreatedNotification : INotification
{
    public DirectMessageCreatedNotification(DirectMessage directMessage)
        => DirectMessage = directMessage;

    public DirectMessage DirectMessage { get; }
}