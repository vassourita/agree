namespace Agree.Accord.Domain.Servers;

using System;
using Microsoft.AspNetCore.Identity;

/// <summary>
/// A role in a server.
/// </summary>
public class ServerRole : IdentityRole<Guid>
{
    // EF ctor
    protected ServerRole() { }

    public ServerRole(string name, Server server) : base(name)
    {
        Server = server;
        ServerId = server.Id;
    }

    public static ServerRole CreateDefaultAdminRole(Server server)
        => new("Admin", server);

    public Guid ServerId { get; private set; }
    public Server Server { get; private set; }
}