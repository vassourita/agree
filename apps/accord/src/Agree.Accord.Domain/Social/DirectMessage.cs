namespace Agree.Accord.Domain.Social;

using System;
using Agree.Accord.Domain.Identity;
using Agree.Accord.SharedKernel;

/// <summary>
/// A direct message between two users.
/// </summary>
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

    /// <summary>
    /// The message id.
    /// </summary>
    /// <value>The message id.</value>
    public Guid Id { get; private set; }

    /// <summary>
    /// The message content.
    /// </summary>
    /// <value>The message text.</value>
    public string Text { get; private set; }

    /// <summary>
    /// The user sending the direct message.
    /// </summary>
    /// <value>The sender user account.</value>
    public UserAccount From { get; private set; }

    /// <summary>
    /// The user receiving the direct message.
    /// </summary>
    /// <value>The receiver user account.</value>
    public UserAccount To { get; private set; }

    /// <summary>
    /// Whether the message has been read by the receiver.
    /// </summary>
    /// <value>true if read, otherwise, false.</value>
    public bool Read { get; private set; }

    /// <summary>
    /// The date the message was sent.
    /// </summary>
    /// <value>The date the message was sent.</value>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Marks the message as read.
    /// </summary>
    public void MarkRead() => Read = true;
}