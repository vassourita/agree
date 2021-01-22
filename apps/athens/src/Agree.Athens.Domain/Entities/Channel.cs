using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Channel : DeletableBaseEntity<Guid>, IAggregateRoot
    {
        public enum ChannelType
        {
            Text, Media
        }
        public string Name { get; set; }
        public int Order { get; set; }
        public ChannelType Type { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}