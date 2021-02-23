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
            Channel = channel;
        }

        protected Message()
        { }

        public string Content { get; private set; }

        public Author Author { get; set; }

        public TextChannel Channel { get; private set; }
    }
}