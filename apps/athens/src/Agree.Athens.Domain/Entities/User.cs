using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Entities.Abstractions;
using Agree.Athens.Domain.Interfaces;

namespace Agree.Athens.Domain.Entities
{
    public class User : DeletableBaseEntity<Guid>, IAggregateRoot
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

        public List<ServerUser> UserServers { get; set; }
        public ICollection<Server> Servers { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}