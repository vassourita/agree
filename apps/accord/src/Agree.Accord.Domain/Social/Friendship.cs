namespace Agree.Accord.Domain.Social;

using System;
using Agree.Accord.Domain.Identity;
using Agree.Accord.SharedKernel;

/// <summary>
/// The representation of a friendship (or a friendship request) between two users.
/// </summary>
public class Friendship : IEntity<string>
{
    // EF ctor
    protected Friendship() { }

    public Friendship(UserAccount from, UserAccount to)
    {
        From = from;
        FromId = from.Id;
        To = to;
        ToId = to.Id;
        Accepted = false;
        SentAt = DateTime.UtcNow;
    }

    public virtual string Id => $"{FromId}_{ToId}";

    public Guid FromId { get; private set; }
    public UserAccount From { get; private set; }
    public Guid ToId { get; private set; }
    public UserAccount To { get; private set; }
    public bool Accepted { get; private set; }

    public DateTime SentAt { get; private set; }
    public DateTime AcceptedAt { get; private set; }

    public void Accept()
    {
        Accepted = true;
        AcceptedAt = DateTime.UtcNow;
    }
}