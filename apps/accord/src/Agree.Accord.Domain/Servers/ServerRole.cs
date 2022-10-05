namespace Agree.Accord.Domain.Servers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Agree.Accord.SharedKernel;

/// <summary>
/// A role in a server.
/// </summary>
public class ServerRole : IEntity<Guid>
{
    // EF ctor
    protected ServerRole() { }

    public ServerRole(string name, Server server)
    {
        Id = Guid.NewGuid();
        Name = name;
        Server = server;
        ServerId = server.Id;
        ServerMembers = new Collection<ServerMember>();
        Permissions = new Collection<ServerRolePermission>();
    }

    /// <summary>
    /// Creates a new role with default admin permissions for a server.
    /// </summary>
    public static ServerRole CreateDefaultAdminRole(Server server)
        => new("Admin", server);

    /// <summary>
    /// The role Id.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// The role name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The server Id this role belongs to.
    /// </summary>
    public Guid ServerId { get; private set; }

    /// <summary>
    /// The server this role belongs to.
    /// </summary>
    public Server Server { get; private set; }

    /// <summary>
    /// The server members with this role.
    /// </summary>
    public ICollection<ServerMember> ServerMembers { get; private set; }

    /// <summary>
    /// The permissions this role has.
    /// </summary>
    public ICollection<ServerRolePermission> Permissions { get; private set; }
}