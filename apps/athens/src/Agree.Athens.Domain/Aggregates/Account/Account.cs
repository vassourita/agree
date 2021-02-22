using Agree.Athens.Domain.Aggregates.Account.Factories;
using Agree.Athens.Domain.Aggregates.Account.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Account
{
    public class Account : Entity, IAggregateRoot
    {
        public Account(string userName, string email, string passwordHash)
        {
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Tag = UserTagFactory.CreateRandomUserTag();
            EmailVerified = false;

            Validate(this, new AccountValidator());
        }

        public void VerifyEmail()
        {
            EmailVerified = true;
        }

        public void UpdateUserName(string newUserName)
        {
            UserName = newUserName;

            Validate(this, new AccountValidator());
        }

        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;

            Validate(this, new AccountValidator());
        }

        public void UpdateTag(UserTag newTag)
        {
            Tag = newTag;
        }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public UserTag Tag { get; private set; }

        public string UserNameWithTag { get => $"{UserName}#{Tag}"; }

        public bool EmailVerified { get; private set; }

        public string PasswordHash { get; private set; }
    }
}