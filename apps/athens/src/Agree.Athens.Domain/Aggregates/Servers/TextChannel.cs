using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Messages;
using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class TextChannel : Entity
    {
        public TextChannel(string name, Server server)
        {
            Messages = new Collection<Message>();
            Name = name;
            Server = server;

            Validate(this, new TextChannelValidator());
        }

        // Empty constructor for EF Core
        protected TextChannel()
        {
            Messages = new Collection<Message>();
        }

        public void UpdateName(string newName)
        {
            Name = newName;

            Validate(this, new TextChannelValidator());
        }

        public string Name { get; protected set; }

        public Server Server { get; protected set; }

        public ICollection<Message> Messages { get; protected set; }
    }
}