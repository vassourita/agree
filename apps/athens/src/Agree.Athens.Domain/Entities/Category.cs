using System.Globalization;
using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace Agree.Athens.Domain.Entities
{
    public class Category : BaseEntity, IAggregateRoot, ISoftDeletable
    {
        public Category(string name, Server server) : base()
        {
            Channels = new Collection<Channel>();

            if (name == null) throw new ArgumentNullException(nameof(name));
            if (server == null) throw new ArgumentNullException(nameof(server));

            Name = name;

            Server = server;
            ServerId = server.Id;

            Order = Server.Categories.Count + 1;
        }

        protected Category()
        {
            Channels = new Collection<Channel>();
        }

        public static Category CreateNewServerDefaultCategory(Server server)
        {
            var category = new Category("Welcome!", server);
            category.Channels.Add(Channel.CreateNewServerDefaultChannel(category));
            return category;
        }

        public string Name { get; set; }
        public int Order { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<Channel> Channels { get; set; }
    }
}