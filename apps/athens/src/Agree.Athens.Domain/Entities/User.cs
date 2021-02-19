using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Interfaces;
using System;
using Agree.Athens.Domain.ValueObjects;

namespace Agree.Athens.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot, ISoftDeletable
    {
        public User(string username, string email, int tag) : base()
        {
            UserServers = new List<ServerUser>();
            Servers = new Collection<Server>();
            Roles = new Collection<Role>();
            Messages = new Collection<Message>();

            if (username == null) throw new ArgumentNullException(nameof(username));
            if (email == null) throw new ArgumentNullException(nameof(email));

            Tag = new UserTag(tag);
            Username = username;
            Email = email;
        }

        public User(string username, string email, string tag) : base()
        {
            UserServers = new List<ServerUser>();
            Servers = new Collection<Server>();
            Roles = new Collection<Role>();
            Messages = new Collection<Message>();

            if (username == null) throw new ArgumentNullException(nameof(username));
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (tag == null) throw new ArgumentNullException(nameof(tag));

            Tag = new UserTag(tag);
            Username = username;
            Email = email;
        }

        public User(string username, string email, UserTag tag) : base()
        {
            UserServers = new List<ServerUser>();
            Servers = new Collection<Server>();
            Roles = new Collection<Role>();
            Messages = new Collection<Message>();

            if (username == null) throw new ArgumentNullException(nameof(username));
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (tag == null) throw new ArgumentNullException(nameof(tag));

            Tag = tag;
            Username = username;
            Email = email;
        }

        protected User()
        {
            UserServers = new List<ServerUser>();
            Servers = new Collection<Server>();
            Roles = new Collection<Role>();
            Messages = new Collection<Message>();
        }

        [MinLength(1)]
        [MaxLength(20)]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public UserTag Tag { get; set; }
        [MaxLength(100)]
        public string Status { get; set; }
        public string Usertag { get => $"{Username}#{Tag}"; }
        public string AvatarFileName { get; set; }
        public DateTime? DeletedAt { get; set; }

        public List<ServerUser> UserServers { get; private set; }
        public ICollection<Server> Servers { get; private set; }
        public ICollection<Role> Roles { get; private set; }
        public ICollection<Message> Messages { get; private set; }
    }
}