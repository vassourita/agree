using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class TextChannel : Entity, IAggregateRoot
    {
        public TextChannel(string name, Server server)
        {
            Name = name;
            Server = server;

            Validate(this, new TextChannelValidator());
        }

        // Empty constructor for EF Core
        protected TextChannel()
        { }

        public void UpdateName(string newName)
        {
            Name = newName;

            Validate(this, new TextChannelValidator());
        }

        public string Name { get; private set; }

        public Server Server { get; private set; }
    }
}