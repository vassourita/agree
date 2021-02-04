using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Interfaces;
using System;

namespace Agree.Athens.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public User()
        {
            UserServers = new List<ServerUser>();
            Servers = new Collection<Server>();
            Messages = new Collection<Message>();
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Tag { get; set; }
        public string Status { get; set; }
        public string Usertag { get => $"{Username}#{Tag}"; }
        public string AvatarFileName { get; set; }

        public DateTime? DeletedAt { get; set; }

        public List<ServerUser> UserServers { get; set; }
        public ICollection<Server> Servers { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}