using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;
using System;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class Server : Entity, IAggregateRoot
    {
        public Server(string name)
        {
            TextChannels = new Collection<TextChannel>();
            Users = new Collection<User>();
            Roles = new Collection<Role>();
            Name = name;

            Validate(this, new ServerValidator());
        }

        // Empty constructor for EF Core
        protected Server()
        {
            TextChannels = new Collection<TextChannel>();
            Users = new Collection<User>();
            Roles = new Collection<Role>();
        }

        public void UpdateName(string newName)
        {
            Name = newName;
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

        public void AddTextChannel(TextChannel channel)
        {
            if (TextChannels.Contains(channel))
            {
                AddError(nameof(TextChannels), $"{channel} already exists {this}", channel);
                return;
            }

            TextChannels.Add(channel);
        }

        public void RemoveTextChannel(TextChannel channel)
        {
            if (!TextChannels.Contains(channel))
            {
                AddError(nameof(TextChannels), $"{channel} does not exists on {this}", channel);
                return;
            }

            TextChannels.Remove(channel);
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

        public ICollection<TextChannel> TextChannels { get; protected set; }

        public ICollection<User> Users { get; protected set; }

        public ICollection<Role> Roles { get; protected set; }
    }
}