using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Interfaces;
using System;
using Agree.Athens.Domain.ValueObjects;

namespace Agree.Athens.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot, ISoftDeletable
    {
        public User(string username, string email, int tag, string passwordHash) : base()
        {
            UserServers = new List<ServerUser>();
            Servers = new Collection<Server>();
            Messages = new Collection<Message>();

            if (username == null) throw new ArgumentNullException(nameof(username));
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));

            Tag = new UserTag(tag);
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }

        public User(string username, string email, string tag, string passwordHash) : base()
        {
            UserServers = new List<ServerUser>();
            Servers = new Collection<Server>();
            Messages = new Collection<Message>();

            if (username == null) throw new ArgumentNullException(nameof(username));
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));

            Tag = new UserTag(tag);
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }

        protected User()
        {
            UserServers = new List<ServerUser>();
            Servers = new Collection<Server>();
            Messages = new Collection<Message>();
        }

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