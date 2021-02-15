using System.ComponentModel.DataAnnotations;
using System;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class Message : BaseEntity, IAggregateRoot
    {
        public Message(string content, User user, Channel channel) : base()
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (channel == null) throw new ArgumentNullException(nameof(channel));

            if (!channel.Category.Server.Users.Contains(user))
            {
                throw UnauthorizedUserException.UserIsNotMemberOfServer(user, channel.Category.Server);
            }

            Content = content;
            User = user;
            UserId = user.Id;
            Channel = channel;
            ChannelId = channel.Id;
        }

        protected Message()
        {
        }

        [MinLength(1)]
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }
    }
}