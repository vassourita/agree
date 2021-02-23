using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Aggregates.Account.Factories;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Messages.Builders
{
    public class AuthorBuilder : IBuilder<Author>
    {
        private string _userName { get; set; } = "";
        private string _email { get; set; } = "";
        private bool _active { get; set; } = true;
        private UserTag _tag { get; set; } = UserTagFactory.CreateRandomUserTag();

        public AuthorBuilder FromUserAccount(UserAccount account)
        {
            _userName = account.UserName;
            _email = account.Email;
            _tag = account.Tag;
            _active = account.DeletedAt is not null;
            return this;
        }

        public Author Build()
        {
            return new Author(_userName, _email, _tag, _active);
        }
    }
}