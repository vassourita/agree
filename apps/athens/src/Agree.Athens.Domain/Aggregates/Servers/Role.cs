using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class Role : Entity
    {
        public Role(string name, ColorHex colorHex, RolePermissions permissions, Server server)
        {
            Name = name;
            ColorHex = colorHex;
            Permissions = permissions;
            Server = server;

            Validate(this, new RoleValidator());
        }

        // Empty constructor for EF Core
        protected Role()
        {
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

        public string Name { get; private set; }

        public ColorHex ColorHex { get; private set; }

        public RolePermissions Permissions { get; private set; }

        public Server Server { get; private set; }
    }
}