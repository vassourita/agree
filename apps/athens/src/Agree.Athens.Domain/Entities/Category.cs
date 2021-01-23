using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace Agree.Athens.Domain.Entities
{
    public class Category : DeletableBaseEntity<Guid>, IAggregateRoot
    {
        public Category()
        {
            Channels = new Collection<Channel>();
        }

        public string Name { get; set; }
        public int Order { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }

        public ICollection<Channel> Channels { get; set; }
    }
}