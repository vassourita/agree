using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Category : DeletableBaseEntity<Guid>, IAggregateRoot
    {
        public string Name { get; set; }
        public int Order { get; set; }

        public ICollection<Channel> Channels { get; set; }
    }
}