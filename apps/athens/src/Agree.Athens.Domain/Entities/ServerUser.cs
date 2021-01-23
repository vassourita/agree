using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace Agree.Athens.Domain.Entities
{
    public class ServerUser : DeletableBaseEntity<int>, IAggregateRoot
    {
        public ServerUser()
        {
            Roles = new Collection<Role>();
        }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ServerId { get; set; }
        public Server Server { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}