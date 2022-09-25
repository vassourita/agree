namespace Agree.Accord.Domain.Identity;

using System.Collections.Generic;
using System;
using Agree.Accord.Domain.Servers;
using Agree.Accord.SharedKernel;
using System.Collections.ObjectModel;

/// <summary>
/// Represents a registered user account.
/// </summary>
public class UserAccount : IEntity<Guid>
{
    // EF ctor
    protected UserAccount() { }

    public UserAccount(string username, string emailAddress, string passwordHash, DiscriminatorTag tag)
    {
        Id = Guid.NewGuid();
        Username = username;
        EmailAddress = emailAddress;
        PasswordHash = passwordHash;
        Tag = tag;
        Servers = new Collection<ServerMember>();
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the user unique identifier.
    /// </summary>
    /// <value>The user unique identifier.</value>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the user email address.
    /// </summary>
    /// <value>The user email address.</value>
    public string EmailAddress { get; private set; }

    /// <summary>
    /// Gets the hashed password.
    /// </summary>
    /// <value>The hashed password.</value>
    public string PasswordHash { get; private set; }

    /// <summary>
    /// Gets the username.
    /// </summary>
    /// <value>The username.</value>
    public string Username { get; private set; }

    /// <summary>
    /// Gets the user nametag. The nametag is a unique identifier formed by the username and the discriminator tag.
    /// </summary>
    /// <value>The user nametag.</value>
    public virtual string NameTag => $"{Username}#{Tag}";

    /// <summary>
    /// Gets the user email discriminator tag.
    /// </summary>
    /// <value>The user discriminator tag.</value>
    public DiscriminatorTag Tag { get; private set; }

    /// <summary>
    /// The servers that the user is a member of.
    /// </summary>
    /// <value>The server list.</value>
    public ICollection<ServerMember> Servers { get; private set; }

    public DateTime CreatedAt { get; private set; }
}