using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace Agree.Athens.Domain.Entities
{
    public class ServerUser : BaseEntity, IAggregateRoot
    {
        public ServerUser()
        {
            Roles = new Collection<Role>();
            ServerUserRoles = new List<ServerUserRole>();
        }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ServerId { get; set; }
        public Server Server { get; set; }


        public List<ServerUserRole> ServerUserRoles { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}