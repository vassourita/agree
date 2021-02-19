using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class ServerUser : BaseEntity, IAggregateRoot
    {
        public ServerUser(Server server, User user, IEnumerable<Role> roles)
        {
            if (server == null) throw new ArgumentNullException(nameof(server));
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (roles == null) throw new ArgumentNullException(nameof(roles));

            Server = server;
            ServerId = server.Id;
            User = user;
            UserId = user.Id;

            foreach (var role in roles)
            {
                User.Roles.Add(role);
            }
        }

        protected ServerUser()
        {
        }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ServerId { get; set; }
        public Server Server { get; set; }
    }
}