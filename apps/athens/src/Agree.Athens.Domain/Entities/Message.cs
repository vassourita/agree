using System;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Message : BaseEntity, IAggregateRoot
    {
        public Message()
        {
        }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }
    }
}