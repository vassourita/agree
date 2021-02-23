using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class Server : Entity, IAggregateRoot
    {
        public Server(string name)
        {
            TextChannels = new Collection<TextChannel>();
            Users = new Collection<User>();
            Name = name;

            Validate(this, new ServerValidator());
        }

        // Empty constructor for EF Core
        protected Server()
        {
            TextChannels = new Collection<TextChannel>();
            Users = new Collection<User>();
        }

        public void UpdateName(string newName)
        {
            Name = newName;

            Validate(this, new ServerValidator());
        }

        public void AddUser(User user)
        {
            if (Users.Contains(user))
            {
                AddError(nameof(Users), $"{user} is already member of {this}", user);
            }

            Users.Add(user);
        }

        public string Name { get; private set; }

        public ICollection<TextChannel> TextChannels { get; private set; }

        public ICollection<User> Users { get; private set; }
    }
}