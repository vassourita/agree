using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.SharedKernel;
using System;

namespace Agree.Athens.Domain.Aggregates.Messages
{
    public class Author : Entity
    {
        public Author(string userName, string email, UserTag tag, bool active)
        {
            Messages = new Collection<Message>();
            UserName = userName;
            Email = email;
            Tag = Tag;
            Active = active;
        }

        // Empty constructor for EF Core
        protected Author()
        {
            Messages = new Collection<Message>();
        }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public UserTag Tag { get; private set; }

        public string UserNameWithTag { get => $"{UserName}#{Tag}"; }

        public bool Active { get; set; }

        public ICollection<Message> Messages { get; private set; }
    }
}