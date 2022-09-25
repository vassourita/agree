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
        Members = new Collection<ServerMember>();
        Roles = new Collection<ServerRole>();
        Categories = new Collection<Category>();
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ServerPrivacy PrivacyLevel { get; private set; }

    public ICollection<ServerMember> Members { get; private set; }
    public ICollection<ServerRole> Roles { get; private set; }
    public ICollection<Category> Categories { get; private set; }
}