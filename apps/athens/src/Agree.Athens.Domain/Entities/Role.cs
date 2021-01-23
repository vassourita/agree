using System;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Role : DeletableBaseEntity<int>, IAggregateRoot
    {
        public Role()
        {
        }

        public string Name { get; set; }
        public string ColorHex { get; set; }
        public int Order { get; set; }

        public bool CanCreateNewRoles { get; set; }
        public bool CanDeleteRoles { get; set; }
        public bool CanDeleteServer { get; set; }
        public bool CanRemoveUsers { get; set; }
        public bool CanUpdateServerAvatar { get; set; }
        public bool CanUpdateServerDescription { get; set; }
        public bool CanUpdateServerName { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }
    }
}