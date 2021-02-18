using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Agree.Athens.Domain.Exceptions;
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
            owner.Roles.Add(defaultAdminRole);

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

        public void AddUser(User user)
        {
            if (Users.Contains(user))
            {
                throw UnauthorizedUserException.UserIsAlreadyInServer(user, this);
            }

            ServerUsers.Add(new ServerUser(this, user, new Role[] { }));
            Users.Add(user);
        }

        public void AddRole(Role role)
        {
            if (Roles.Contains(role))
            {
                throw InvalidRoleException.RoleAlreadyExists(role, this);
            }

            Roles.Add(role);
        }

        [MinLength(1)]
        [MaxLength(40)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string AvatarFileName { get; set; }

        public List<ServerUser> ServerUsers { get; private set; }
        public ICollection<User> Users { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        public ICollection<Category> Categories { get; private set; }
    }
}