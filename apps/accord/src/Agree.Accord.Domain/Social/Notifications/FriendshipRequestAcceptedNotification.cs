namespace Agree.Accord.Domain.Social.Notifications;

using MediatR;

public class FriendshipRequestAcceptedNotification : INotification
{
    public FriendshipRequestAcceptedNotification(Friendship friendship)
        => Friendship = friendship;

    public Friendship Friendship { get; }
}