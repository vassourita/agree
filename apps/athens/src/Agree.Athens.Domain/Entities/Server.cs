using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Server : BaseEntity, IAggregateRoot
    {
        public Server(string name, User owner) : base()
        {
            ServerUsers = new List<ServerUser>();
            Users = new Collection<User>();
            Roles = new Collection<Role>();
            Categories = new Collection<Category>();

            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;
            Description = string.Empty;

            var defaultAdminRole = Role.CreateDefaultOwnerRole(this);
            Roles.Add(defaultAdminRole);

            ServerUsers.Add(new ServerUser(this, owner, new Role[] { defaultAdminRole }));
            Users.Add(owner);
            Categories.Add(Category.CreateNewServerDefaultCategory(this));
        }

        protected Server()
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