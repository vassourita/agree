using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;
using System;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class Role : Entity
    {
        public Role(string name, ColorHex colorHex, RolePermissions permissions, Server server)
        {
            Users = new Collection<User>();
            Name = name;
            ColorHex = colorHex;
            CanUpdateServerName = permissions.CanUpdateServerName;
            CanDeleteServer = permissions.CanDeleteServer;
            CanAddUsers = permissions.CanAddUsers;
            CanRemoveUsers = permissions.CanRemoveUsers;
            Server = server;

            Validate(this, new RoleValidator());
        }

        // Empty constructor for EF Core
        protected Role()
        {
            Users = new Collection<User>();
        }

        public void UpdateName(string newName)
        {
            Name = newName;
            UpdatedAt = DateTime.UtcNow;

            Validate(this, new RoleValidator());
        }

        public void UpdateColorHex(ColorHex newColorHex)
        {
            ColorHex = newColorHex;
            UpdatedAt = DateTime.UtcNow;

            Validate(this, new RoleValidator());
        }

        public string Name { get; protected set; }

        public ColorHex ColorHex { get; protected set; }

        public bool CanUpdateServerName { get; set; } = false;
        public bool CanDeleteServer { get; set; } = false;
        public bool CanAddUsers { get; set; } = false;
        public bool CanRemoveUsers { get; set; } = false;

        public Server Server { get; protected set; }
        public Guid ServerId { get; protected set; }

        public ICollection<User> Users { get; protected set; }
    }
}