using Agree.Accord.SharedKernel;

namespace Agree.Accord.Domain.Identity
{
    /// <summary>
    /// Represents a registered user account.
    /// </summary>
    public class UserAccount : Entity
    {
        /// <summary>
        /// Represents a registered user account.
        /// </summary>
        /// <param name="userName">The user display name.</param>
        /// <param name="email">The user email address.</param>
        /// <param name="tag">The user discriminator tag.</param>
        public UserAccount(string userName, string email, DiscriminatorTag tag)
        {
            UserName = userName;
            Email = email;
            Tag = tag;
        }

        public void UpdateUserName(string newUserName)
        {
            UserName = newUserName;
            Update();
        }

        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
            Update();
        }

        public string UserName { get; private set; }
        public string Email { get; private set; }
        public DiscriminatorTag Tag { get; private set; }
    }
}