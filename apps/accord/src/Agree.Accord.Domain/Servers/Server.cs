namespace Agree.Accord.Domain.Servers;

using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using Agree.Accord.SharedKernel;

/// <summary>
/// A message server.
/// </summary>
public class Server : IEntity<Guid>
{
    /// EF ctor
    protected Server()
    {
        Members = new Collection<ServerMember>();
        Roles = new Collection<ServerRole>();
        Categories = new Collection<Category>();
    }

    public Server(string name, ServerPrivacy privacyLevel, string description = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        PrivacyLevel = privacyLevel;
        CreatedAt = DateTime.UtcNow;
        Members = new Collection<ServerMember>();
        Roles = new Collection<ServerRole>();
        Categories = new Collection<Category>();
    }

    /// <summary>
    /// The server id.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// The server name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The server description.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// The server privacy level.
    /// </summary>
    public ServerPrivacy PrivacyLevel { get; private set; }

    /// <summary>
    /// The server creation date.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// The server members in a N-N pivot collection.
    /// </summary>
    public ICollection<ServerMember> Members { get; private set; }

    /// <summary>
    /// The server roles.
    /// </summary>
    public ICollection<ServerRole> Roles { get; private set; }

    /// <summary>
    /// The server categories.
    /// </summary>
    public ICollection<Category> Categories { get; private set; }
}