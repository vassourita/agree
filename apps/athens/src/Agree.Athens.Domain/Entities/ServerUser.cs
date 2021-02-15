using System.Linq;
using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Interfaces;
using System.Collections.ObjectModel;
using Agree.Athens.Domain.Exceptions;

namespace Agree.Athens.Domain.Entities
{
    public class ServerUser : BaseEntity, IAggregateRoot
    {
        public ServerUser(Server server, User user, Role[] roles)
        {
            Roles = new Collection<Role>();
            ServerUserRoles = new List<ServerUserRole>();

            if (server == null) throw new ArgumentNullException(nameof(server));
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (roles == null) throw new ArgumentNullException(nameof(roles));

            Server = server;
            ServerId = server.Id;
            User = user;
            UserId = user.Id;

            foreach (var role in roles)
            {
                ServerUserRoles.Add(new ServerUserRole(this, role));
                Roles.Add(role);
            }
        }

        protected ServerUser()
        {
            Roles = new Collection<Role>();
            ServerUserRoles = new List<ServerUserRole>();
        }

        public void AddRole(Role role, User user)
        {
            if (!Server.Roles.Contains(role))
            {
                throw InvalidRoleException.RoleIsNotFromServer(role, Server);
            }
            if (Roles.Contains(role))
            {
                throw InvalidRoleException.UserAlreadyHasRole(role, user);
            }

            Roles.Add(role);
        }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ServerId { get; set; }
        public Server Server { get; set; }

        public List<ServerUserRole> ServerUserRoles { get; private set; }
        public ICollection<Role> Roles { get; private set; }
    }
}