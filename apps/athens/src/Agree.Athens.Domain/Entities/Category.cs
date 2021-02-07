using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace Agree.Athens.Domain.Entities
{
    public class Category : BaseEntity, IAggregateRoot, ISoftDeletable
    {
        public Category()
        {
            Channels = new Collection<Channel>();
        }

        public string Name { get; set; }
        public int Order { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<Channel> Channels { get; set; }
    }
}