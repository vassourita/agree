using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Interfaces;
using Agree.Athens.Domain.Exceptions;

namespace Agree.Athens.Domain.Entities
{
    public class Channel : BaseEntity, IAggregateRoot, ISoftDeletable
    {
        public Channel(string name, Category category, ChannelType channelType = ChannelType.Text) : base()
        {
            Messages = new Collection<Message>();

            if (name == null) throw new ArgumentNullException(nameof(name));
            if (category == null) throw new ArgumentNullException(nameof(category));

            Name = name;

            Type = channelType;
            Category = category;
            CategoryId = category.Id;

            Order = Category.Channels.Count + 1;
        }

        public static Channel CreateNewServerDefaultChannel(Category category)
        {
            return new Channel("Welcome!", category);
        }

        public Message AddMessage(string content, User author)
        {
            if (Type == ChannelType.Media)
            {
                throw InvalidChannelTypeException.ChannelCannotReceiveMessages();
            }

            var message = new Message(content, author, this);
            Messages.Add(message);
            return message;
        }

        protected Channel()
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

        public ICollection<Message> Messages { get; private set; }
    }
}