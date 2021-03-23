using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;
using System;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class Server : Entity, IAggregateRoot
    {
        public Server(string name, string description)
        {
            Categories = new Collection<Category>();
            Users = new Collection<User>();
            Roles = new Collection<Role>();
            Name = name;
            Description = description;

            Validate(this, new ServerValidator());
        }

        // Empty constructor for EF Core
        protected Server()
        {
            Categories = new Collection<Category>();
            Users = new Collection<User>();
            Roles = new Collection<Role>();
        }

        public void UpdateName(string newName)
        {
            Name = newName;
            UpdatedAt = DateTime.UtcNow;

            Validate(this, new ServerValidator());
        }

        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
            UpdatedAt = DateTime.UtcNow;

            Validate(this, new ServerValidator());
        }

        public void AddUser(User user)
        {
            if (Users.Contains(user))
            {
                AddError(nameof(Users), $"{user} is already member of {this}", user);
                return;
            }

            Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            if (!Users.Contains(user))
            {
                AddError(nameof(Users), $"{user} is not member of {this}", user);
                return;
            }

            Users.Remove(user);
        }

        public void AddCategory(Category channel)
        {
            if (Categories.Contains(channel))
            {
                AddError(nameof(Categories), $"{channel} already exists {this}", channel);
                return;
            }

            Categories.Add(channel);
        }

        public void RemoveCategory(Category channel)
        {
            if (!Categories.Contains(channel))
            {
                AddError(nameof(Categories), $"{channel} does not exists on {this}", channel);
                return;
            }

            Categories.Remove(channel);
        }

        public void AddRole(Role role)
        {
            if (Roles.Contains(role))
            {
                AddError(nameof(Roles), $"{role} already exists on {this}", role);
                return;
            }

            Roles.Add(role);
        }

        public void RemoveRole(Role role)
        {
            if (!Roles.Contains(role))
            {
                AddError(nameof(Roles), $"{role} does not exists of {this}", role);
                return;
            }

            Roles.Remove(role);
        }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public ICollection<Category> Categories { get; protected set; }

        public ICollection<User> Users { get; protected set; }

        public ICollection<Role> Roles { get; protected set; }
    }
}