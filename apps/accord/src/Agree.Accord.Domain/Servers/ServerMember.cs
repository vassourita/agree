namespace Agree.Accord.Domain.Servers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Agree.Accord.Domain.Identity;
using Agree.Accord.SharedKernel;

/// <summary>
/// Represents a member of a server.
/// Works as a pivot entity between <see cref="Server"/> and <see cref="User"/>.
/// </summary>
public class ServerMember : IEntity<string>
{
    // EF ctor
    protected ServerMember() { }

    public ServerMember(UserAccount user, Server server)
    {
        User = user;
        UserId = user.Id;
        Server = server;
        ServerId = server.Id;
        Roles = new Collection<ServerRole>();
    }

    public virtual string Id => $"{UserId}_{ServerId}";
    public Guid UserId { get; private set; }
    public UserAccount User { get; private set; }
    public Guid ServerId { get; private set; }
    public Server Server { get; private set; }
    public ICollection<ServerRole> Roles { get; private set; }
}