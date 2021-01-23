using System;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class ServerUserRole : BaseEntity<int>, IAggregateRoot
    {
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public ServerUser ServerUser { get; set; }
        public int ServerUserId { get; set; }
    }
}