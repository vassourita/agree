using System;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Message : DeletableBaseEntity<long>, IAggregateRoot
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }
    }
}