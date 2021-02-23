using System.Collections.Generic;
using System.Collections.ObjectModel;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class User : Entity
    {
        public User(string userName, string email, UserTag tag, Server server, IEnumerable<Role> roles)
        {
            Roles = new Collection<Role>();

            UserName = userName;
            Email = email;
            Tag = Tag;
            Server = server;

            foreach (var role in roles)
            {
                Roles.Add(role);
            }
        }

        // Empty constructor for EF Core
        protected User()
        {
            Roles = new Collection<Role>();
        }

        public void AddRole(Role role)
        {
            if (!Server.Roles.Contains(role))
            {
                AddError(nameof(Roles), $"{role} does not exists in {Server}", role);
            }
            if (Roles.Contains(role))
            {
                AddError(nameof(Roles), $"{this} already has {role}", role);
            }

            Roles.Add(role);
        }

        public void RemoveRole(Role role)
        {
            if (!Roles.Contains(role))
            {
                AddError(nameof(Roles), $"{this} does not have {role}", role);
            }

            Roles.Remove(role);
        }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public UserTag Tag { get; private set; }

        public string UserNameWithTag { get => $"{UserName}#{Tag}"; }

        public Server Server { get; private set; }

        public ICollection<Role> Roles { get; private set; }
    }
}