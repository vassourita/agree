using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Channel : BaseEntity, IAggregateRoot, ISoftDeletable
    {
        public Channel()
        {
            Messages = new Collection<Message>();
        }

        public enum ChannelType
        {
            Text, Media
        }

        public string Name { get; set; }
        public int Order { get; set; }
        public ChannelType Type { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}