namespace Agree.Accord.Domain.Social.Notifications;

using MediatR;

public class FriendshipRequestCreatedNotification : INotification
{
    public FriendshipRequestCreatedNotification(Friendship friendship)
        => Friendship = friendship;

    public Friendship Friendship { get; }
}