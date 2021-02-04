using System;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class ServerUserRole : BaseEntity, IAggregateRoot
    {
        public Role Role { get; set; }
        public Guid RoleId { get; set; }
        public ServerUser ServerUser { get; set; }
        public Guid ServerUserId { get; set; }
    }
}