using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class Server : Entity, IAggregateRoot
    {
        public Server(string name)
        {
            Name = name;

            Validate(this, new ServerValidator());
        }

        // Empty constructor for EF Core
        protected Server()
        { }

        public string Name { get; private set; }
    }
}