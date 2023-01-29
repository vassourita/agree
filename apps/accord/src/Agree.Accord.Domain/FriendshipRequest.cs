namespace Agree.Accord.Domain;

using System;
using Agree.SharedKernel;

public class FriendshipRequest : IEntity<Guid>
{
    public FriendshipRequest(User from, User to)
    {
        Id = Guid.NewGuid();
        From = from;
        To = to;
        Accepted = false;
        SentAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public User From { get; private set; }
    public User To { get; private set; }
    public bool Accepted { get; private set; }

    public DateTime SentAt { get; private set; }
    public DateTime AcceptedAt { get; private set; }

    public void Accept(User acceptingUser)
    {
        if (Accepted)
            throw new FriendshipAlreadyAcceptedException();
        if (acceptingUser != To)
            throw new FriendshipRequestNotForUserException();
        Accepted = true;
        AcceptedAt = DateTime.UtcNow;
    }
}