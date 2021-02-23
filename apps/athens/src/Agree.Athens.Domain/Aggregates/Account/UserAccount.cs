using System;
using Agree.Athens.Domain.Aggregates.Account.Factories;
using Agree.Athens.Domain.Aggregates.Account.Validators;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Aggregates.Account
{
    public class UserAccount : Entity, IAggregateRoot, ISoftDeletable
    {
        public UserAccount(string userName, string email, string passwordHash)
        {
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Tag = UserTagFactory.CreateRandomUserTag();
            EmailVerified = false;

            Validate(this, new UserAccountValidator());
        }

        // Empty constructor for EF Core
        protected UserAccount()
        { }

        public void VerifyEmail()
        {
            EmailVerified = true;
        }

        public void UpdateUserName(string newUserName)
        {
            UserName = newUserName;

            Validate(this, new UserAccountValidator());
        }

        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;

            Validate(this, new UserAccountValidator());
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

        public DateTime? DeletedAt { get; set; }
    }
}