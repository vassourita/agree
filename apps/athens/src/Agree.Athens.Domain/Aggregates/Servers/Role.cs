using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class Role : Entity
    {
        public Role(string name, ColorHex colorHex, RolePermissions permissions, Server server)
        {
            Users = new Collection<User>();
            Name = name;
            ColorHex = colorHex;
            Permissions = permissions;
            Server = server;

            Validate(this, new RoleValidator());
        }

        // Empty constructor for EF Core
        protected Role()
        {
            Users = new Collection<User>();
            Permissions = new RolePermissions();
        }

        public void UpdateName(string newName)
        {
            Name = newName;

            Validate(this, new RoleValidator());
        }

        public void UpdateColorHex(ColorHex newColorHex)
        {
            ColorHex = newColorHex;

            Validate(this, new RoleValidator());
        }

        public string Name { get; protected set; }

        public ColorHex ColorHex { get; protected set; }

        public RolePermissions Permissions { get; protected set; }

        public Server Server { get; protected set; }

        public ICollection<User> Users { get; protected set; }
    }
}