namespace Agree.Accord.Domain.Servers;

using System;
using Agree.Accord.SharedKernel;

public class ServerInvite : IEntity<Guid>
{
    public Guid Id { get; private set; }
    public Server Server { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsExpired => ExpirationDate < DateTime.UtcNow;

    public ServerInvite(Server server, DateTime expirationDate)
    {
        Id = Guid.NewGuid();
        Server = server;
        ExpirationDate = expirationDate;
    }
}