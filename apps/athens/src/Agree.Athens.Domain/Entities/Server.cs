using System;
using System.Collections.Generic;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Server : DeletableBaseEntity<Guid>, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AvatarFileName { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}