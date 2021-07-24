using System.Collections.Generic;
using System;
using Agree.Accord.Domain.Identity;

namespace Agree.Accord.Domain.Servers
{
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
            => new Category($"Welcome to {server.Name}!", server);
    }
}