namespace Agree.Accord.Domain.Social.Notifications;

using MediatR;

public class FriendshipRequestDeclinedNotification : INotification
{
    public FriendshipRequestDeclinedNotification(Friendship friendship)
        => Friendship = friendship;

    public Friendship Friendship { get; }
}