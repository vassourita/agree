using System;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class ServerUserRole : BaseEntity, IAggregateRoot
    {
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public ServerUser ServerUser { get; set; }
        public int ServerUserId { get; set; }
    }
}