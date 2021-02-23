using System.Collections.Generic;
using System.Collections.ObjectModel;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class User : Entity, IAggregateRoot
    {
        public User(string userName, string email, UserTag tag, Server server)
        {
            UserName = userName;
            Email = email;
            Tag = Tag;
            Server = server;
        }

        // Empty constructor for EF Core
        protected User()
        {
        }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public UserTag Tag { get; private set; }

        public string UserNameWithTag { get => $"{UserName}#{Tag}"; }

        public Server Server { get; private set; }
    }
}