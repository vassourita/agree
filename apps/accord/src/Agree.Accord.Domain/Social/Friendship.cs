namespace Agree.Accord.Domain.Social;

using System;
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

    /// <summary>
    /// The friendship id.
    /// </summary>
    /// <value>The friendship id (a composite key).</value>
    public virtual string Id => $"{FromId}_{ToId}";

    /// <summary>
    /// The user sending the friendship request.
    /// </summary>
    /// <value>The sender user account id.</value>
    public Guid FromId { get; private set; }

    /// <summary>
    /// The user sending the friendship request.
    /// </summary>
    /// <value>The sender user account.</value>
    public UserAccount From { get; private set; }

    /// <summary>
    /// The user receiving the friendship request.
    /// </summary>
    /// <value>The receiver user account id.</value>
    public Guid ToId { get; private set; }

    /// <summary>
    /// The user receiving the friendship request.
    /// </summary>
    /// <value>The receiver user account.</value>
    public UserAccount To { get; private set; }

    /// <summary>
    /// Whether the friendship has been accepted or not by the receiver.
    /// </summary>
    /// <value>true if accepted, otherwise, false.</value>
    public bool Accepted { get; private set; }

    /// <summary>
    /// The date the friendship request was sent.
    /// </summary>
    /// <value>The date the friendship request was sent.</value>
    public DateTime SentAt { get; private set; }

    /// <summary>
    /// Accepts the friendship request.
    /// </summary>
    public DateTime AcceptedAt { get; private set; }

    /// <summary>
    /// Accepts the friendship request.
    /// </summary>
    public void Accept()
    {
        if (Accepted)
            return;
        Accepted = true;
        AcceptedAt = DateTime.UtcNow;
    }
}