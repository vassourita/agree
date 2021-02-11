using System;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class ServerUserRole : BaseEntity, IAggregateRoot
    {
        public ServerUserRole(ServerUser serverUser, Role role)
        {
            if (serverUser == null) throw new ArgumentNullException(nameof(serverUser));
            if (role == null) throw new ArgumentNullException(nameof(role));

            Role = role;
            RoleId = role.Id;
            ServerUser = serverUser;
            ServerUserId = serverUser.Id;
        }

        protected ServerUserRole()
        { }

        public Role Role { get; set; }
        public Guid RoleId { get; set; }
        public ServerUser ServerUser { get; set; }
        public Guid ServerUserId { get; set; }
    }
}