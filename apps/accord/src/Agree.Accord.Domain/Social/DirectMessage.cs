namespace Agree.Accord.Domain.Social;

using System;
using Agree.Accord.Domain.Identity;
using Agree.Accord.SharedKernel;

public class DirectMessage : IEntity<Guid>
{
    // EF ctor
    public DirectMessage() { }

    public DirectMessage(string text, UserAccount from, UserAccount to)
    {
        Id = Guid.NewGuid();
        Text = text;
        From = from;
        To = to;
        Read = false;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public string Text { get; private set; }
    public UserAccount From { get; private set; }
    public UserAccount To { get; private set; }
    public bool Read { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public void MarkRead() => Read = true;
}