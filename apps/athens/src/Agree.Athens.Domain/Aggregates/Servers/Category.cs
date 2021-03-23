using System.Collections.Generic;
using System.Collections.ObjectModel;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class Category : Entity
    {
        public Category(string name, Server server)
        {
            Name = name;
            Server = server;

            TextChannels = new Collection<TextChannel>();
        }

        // Empty constructor for EF Core
        protected Category()
        {
            TextChannels = new Collection<TextChannel>();
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

        public string Name { get; protected set; }

        public Server Server { get; protected set; }

        public ICollection<TextChannel> TextChannels { get; protected set; }
    }
}