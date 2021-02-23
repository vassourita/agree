using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Messages
{
    public class Author : Entity
    {
        public Author(string userName, string email, UserTag tag)
        {
            UserName = userName;
            Email = email;
            Tag = Tag;
        }

        // Empty constructor for EF Core
        protected Author()
        { }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public UserTag Tag { get; private set; }

        public string UserNameWithTag { get => $"{UserName}#{Tag}"; }
    }
}