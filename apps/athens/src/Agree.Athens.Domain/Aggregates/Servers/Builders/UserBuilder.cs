using System.Linq;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.SharedKernel;
using Agree.Athens.Domain.Aggregates.Account.Factories;

namespace Agree.Athens.Domain.Aggregates.Servers.Factories
{
    public class UserBuilder : IBuilder<User>
    {
        private string _userName { get; set; } = "";
        private string _email { get; set; } = "";
        private bool _active { get; set; } = true;
        private UserTag _tag { get; set; } = UserTagFactory.CreateRandomUserTag();
        private Server _server { get; set; }
        public IEnumerable<Role> _roles { get; private set; } = new List<Role>();

        public UserBuilder FromUserAccount(UserAccount account)
        {
            _userName = account.UserName;
            _email = account.Email;
            _tag = account.Tag;
            _active = account.DeletedAt is not null;
            return this;
        }

        public UserBuilder IsMemberOfServer(Server server)
        {
            _server = server;
            return this;
        }

        public UserBuilder HasRole(Role role)
        {
            _roles.Append(role);
            return this;
        }

        public UserBuilder HasRoles(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                _roles.Append(role);
            }
            return this;
        }

        public User Build()
        {
            return new User(_userName, _email, _tag, _server, _roles, _active);
        }
    }
}