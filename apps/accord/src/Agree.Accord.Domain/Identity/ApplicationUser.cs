namespace Agree.Accord.Domain.Identity;

using System.Collections.Generic;
using System;
using Agree.Accord.Domain.Servers;

/// <summary>
/// Represents a registered user account.
/// </summary>
public class ApplicationUser
{
    // EF ctor
    public ApplicationUser() { }

    /// <summary>
    /// Gets the user unique identifier.
    /// </summary>
    /// <value>The user unique identifier.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the user email address.
    /// </summary>
    /// <value>The user email address.</value>
    public string EmailAddress { get; set; }

    /// <summary>
    /// Gets whether the user email address is confirmed or not.
    /// </summary>
    /// <value>True if user email address is confirmed, false otherwise.</value>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Gets the username.
    /// </summary>
    /// <value>The username.</value>
    public string Username { get; set; }

    /// <summary>
    /// Gets the user nametag. The nametag is a unique identifier formed by the username and the discriminator tag.
    /// </summary>
    /// <value>The user nametag.</value>
    public virtual string NameTag => $"{Username}#{Tag}";

    /// <summary>
    /// Gets the user email discriminator tag.
    /// </summary>
    /// <value>The user discriminator tag.</value>
    public DiscriminatorTag Tag { get; set; }

    /// <summary>
    /// The servers that the user is a member of.
    /// </summary>
    /// <value>The server list.</value>
    public ICollection<Server> Servers { get; set; }

    public DateTime CreatedAt { get; set; }
}