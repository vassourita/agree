using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Server : DeletableBaseEntity<Guid>, IAggregateRoot
    {
        public Server()
        {
            ServerUsers = new List<ServerUser>();
            Users = new Collection<User>();
            Roles = new Collection<Role>();
            Categories = new Collection<Category>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string AvatarFileName { get; set; }

        public List<ServerUser> ServerUsers { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}