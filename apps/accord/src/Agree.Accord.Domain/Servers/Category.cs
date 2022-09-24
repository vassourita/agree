namespace Agree.Accord.Domain.Servers;

using System;

/// <summary>
/// A server category.
/// </summary>
public class Category
{
    /// EF ctor
    protected Category() { }

    public Category(string name, Server server)
    {
        Id = Guid.NewGuid();
        Name = name;
        Server = server;
        ServerId = server.Id;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }

    public Guid ServerId { get; set; }
    public Server Server { get; set; }

    public static Category CreateDefaultWelcomeCategory(Server server)
        => new($"Welcome to {server.Name}!", server);
}