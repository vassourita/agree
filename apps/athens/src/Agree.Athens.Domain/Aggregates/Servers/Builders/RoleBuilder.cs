using Agree.Athens.Domain.Aggregates.Servers.Factories;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers.Builders
{
    public class RoleBuilder : IBuilder<Role>
    {
        private string _name { get; set; } = "";

        private ColorHex _colorHex { get; set; }

        private RolePermissions _permissions { get; set; } = new RolePermissions();

        private Server _server { get; set; }

        public RoleBuilder HasRandomColorHex()
        {
            _colorHex = ColorHexFactory.CreateRandomColorHex();
            return this;
        }

        public RoleBuilder HasPermissions(RolePermissions permissions)
        {
            _permissions = permissions;
            return this;
        }

        public RoleBuilder HasDefaultOwnerPermissions()
        {
            _permissions = new RolePermissions()
            {
                CanUpdateServerName = true,
                CanDeleteServer = true,
                CanAddUsers = true,
                CanRemoveUsers = true
            };
            return this;
        }

        public RoleBuilder HasName(string name)
        {
            _name = name;
            return this;
        }

        public RoleBuilder BelongsToServer(Server server)
        {
            _server = server;
            return this;
        }

        public Role Build()
        {
            return new Role(_name, _colorHex, _permissions, _server);
        }
    }
}