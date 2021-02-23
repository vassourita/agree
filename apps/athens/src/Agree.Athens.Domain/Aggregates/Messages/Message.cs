using System;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Messages
{
    public class Message : Entity, IAggregateRoot
    {
        public Message(string content, Author author, TextChannel channel)
        {
            Content = content;
            Author = author;
            AuthorId = author.Id;
            Channel = channel;
            ChannelId = channel.Id;
        }

        protected Message()
        { }

        public string Content { get; private set; }

        public Author Author { get; set; }

        public Guid AuthorId { get; set; }

        public TextChannel Channel { get; private set; }

        public Guid ChannelId { get; private set; }
    }
}