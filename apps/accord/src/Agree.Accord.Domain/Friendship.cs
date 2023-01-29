namespace Agree.Accord.Domain;

using System;
using Agree.SharedKernel;

public class Friendship : IEntity<Guid>
{
    public Friendship(User from, User to)
    {
        Id = Guid.NewGuid();
        From = from;
        To = to;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public User From { get; private set; }
    public User To { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime AcceptedAt { get; private set; }
}