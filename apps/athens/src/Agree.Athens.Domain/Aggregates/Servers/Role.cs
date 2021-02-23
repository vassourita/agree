using Agree.Athens.Domain.Aggregates.Servers.Factories;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class Role : Entity, IAggregateRoot
    {
        public Role(string name)
        {
            Name = name;
            ColorHex = ColorHexFactory.CreateRandomColorHex();
        }

        // Empty constructor for EF Core
        protected Role()
        { }

        public string Name { get; private set; }

        public ColorHex ColorHex { get; private set; }

        public Server Server { get; private set; }
    }
}