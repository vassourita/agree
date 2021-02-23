using Agree.Athens.Domain.Aggregates.Account;

namespace Agree.Athens.Domain.Aggregates.Servers.Factories
{
    public class UserBuilder
    {
        private string _userName { get; set; }
        private string _email { get; set; }
        private UserTag _tag { get; set; }
        private Server _server { get; set; }

        public UserBuilder FromUserAccount(UserAccount account)
        {
            _userName = account.UserName;
            _email = account.Email;
            _tag = account.Tag;
            return this;
        }

        public UserBuilder IsMemberOfServer(Server server)
        {
            _server = server;
            return this;
        }

        public User Build()
        {
            return new User(_userName, _email, _tag, _server);
        }
    }
}