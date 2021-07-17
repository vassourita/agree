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
        public UserAccount(string userName, string email, string passwordHash, DiscriminatorTag tag)
        {
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Tag = tag;
        }

        /// <summary>
        /// Updates the user display name.
        /// </summary>
        /// <param name="newUserName">The new user display name.</param>
        public void UpdateUserName(string newUserName)
        {
            UserName = newUserName;
            Update();
        }

        /// <summary>
        /// Updates the user email.
        /// </summary>
        /// <param name="newUserName">The new user email.</param>
        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
            Update();
        }

        /// <summary>
        /// Gets the user display name.
        /// </summary>
        /// <value>The user display name.</value>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the user nametag. The nametag is a unique identifier formed by the user display name and the discriminator tag.
        /// </summary>
        /// <value>The user nametag.</value>
        public string NameTag { get => $"{UserName}#{Tag.ToString()}"; }

        /// <summary>
        /// Gets the user email address.
        /// </summary>
        /// <value>The user email.</value>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the user hashed password.
        /// </summary>
        /// <value>The user hashed password.</value>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Gets the user email discriminator tag.
        /// </summary>
        /// <value>The user discriminator tag.</value>
        public DiscriminatorTag Tag { get; private set; }
    }
}