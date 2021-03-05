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
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateUserName(string newUserName)
        {
            UserName = newUserName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateTag(UserTag newTag)
        {
            Tag = newTag;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateAvatar(string avatarUrl)
        {
            AvatarUrl = avatarUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        public string UserName { get; protected set; }

        public string Email { get; protected set; }

        public UserTag Tag { get; protected set; }

        public string UserNameWithTag { get => $"{UserName}#{Tag}"; }

        public bool EmailVerified { get; protected set; }

        public string PasswordHash { get; protected set; }

        public string AvatarUrl { get; protected set; }

        public void SoftDelete()
        {
            DeletedAt = DateTime.UtcNow;
        }

        public DateTime? DeletedAt { get; protected set; }
    }
}