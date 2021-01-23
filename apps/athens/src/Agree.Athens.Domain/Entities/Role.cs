using System;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Security;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Role : DeletableBaseEntity<int>, IAggregateRoot
    {
        public Role()
        {
            Permissions = new RolePermissions();
        }

        public string Name { get; set; }
        public string ColorHex { get; set; }
        public int Order { get; set; }

        public RolePermissions Permissions { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }
    }
}