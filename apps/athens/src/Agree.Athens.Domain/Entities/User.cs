using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Interfaces;
using System;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.ValueObjects;

namespace Agree.Athens.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public User(Guid id, string username, string email, int tag, string password)
        {
            UserServers = new List<ServerUser>();
            Servers = new Collection<Server>();
            Messages = new Collection<Message>();

            if (id == null || id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Id = id;
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Tag = new UserTag(tag);
            PasswordHash = password ?? throw new ArgumentNullException(nameof(password));
        }

        public User(Guid id, string username, string email, string tag, string password)
        {
            UserServers = new List<ServerUser>();
            Servers = new Collection<Server>();
            Messages = new Collection<Message>();

            if (id == null || id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Id = id;
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Tag = new UserTag(tag);
            PasswordHash = password ?? throw new ArgumentNullException(nameof(password));
        }

        protected User() { }

        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserTag Tag { get; set; }
        public string Status { get; set; }
        public string Usertag { get => $"{Username}#{Tag}"; }
        public string AvatarFileName { get; set; }

        public DateTime? DeletedAt { get; set; }

        public List<ServerUser> UserServers { get; set; }
        public ICollection<Server> Servers { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}