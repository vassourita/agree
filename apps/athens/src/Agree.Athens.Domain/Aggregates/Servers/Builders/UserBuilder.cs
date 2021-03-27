using System.Linq;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.SharedKernel;
using Agree.Athens.Domain.Aggregates.Account.Factories;
using System;

namespace Agree.Athens.Domain.Aggregates.Servers.Builders
{
    public class UserBuilder : IBuilder<User>
    {
        private Guid _id { get; set; } = Guid.NewGuid();
        private string _userName { get; set; } = "";
        private string _email { get; set; } = "";
        private bool _active { get; set; } = true;
        private UserTag _tag { get; set; } = UserTagFactory.CreateRandomUserTag();
        private Server _server { get; set; }
        private ICollection<Role> _roles { get; set; } = new List<Role>();

        public UserBuilder FromUserAccount(UserAccount account)
        {
            _userName = account.UserName;
            _email = account.Email;
            _tag = account.Tag;
            _active = account.DeletedAt is not null;
            _id = account.Id;
            return this;
        }

        public UserBuilder IsMemberOfServer(Server server)
        {
            _server = server;
            return this;
        }

        public UserBuilder HasRole(Role role)
        {
            _roles.Add(role);
            return this;
        }

        public UserBuilder HasRoles(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                _roles.Add(role);
            }
            return this;
        }

        public User Build()
        {
            var user = new User(_userName, _email, _tag, _server, _roles, _active);
            user.SetId(_id);
            return user;
        }
    }
}