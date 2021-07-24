using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using Agree.Accord.Domain.Identity;

namespace Agree.Accord.Domain.Servers
{
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
            => new ServerRole("Admin", server);

        public Guid ServerId { get; private set; }
        public Server Server { get; private set; }
    }
}