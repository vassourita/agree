namespace Agree.Accord.Domain.Servers;

using System;
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
    }

    public static ServerRole CreateDefaultAdminRole(Server server)
        => new("Admin", server);

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid ServerId { get; private set; }
    public Server Server { get; private set; }
}