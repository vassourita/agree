using System.Collections.Generic;
using System;
using Agree.Accord.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Agree.Accord.Domain.Servers;

namespace Agree.Accord.Domain.Identity
{
    /// <summary>
    /// Represents a registered user account.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        // EF ctor
        public ApplicationUser() { }

        /// <summary>
        /// Gets the user display name.
        /// </summary>
        /// <value>The user display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the user nametag. The nametag is a unique identifier formed by the user display name and the discriminator tag.
        /// </summary>
        /// <value>The user nametag.</value>
        public virtual string NameTag { get => $"{DisplayName}#{Tag.ToString()}"; }

        /// <summary>
        /// Gets the user email discriminator tag.
        /// </summary>
        /// <value>The user discriminator tag.</value>
        public DiscriminatorTag Tag { get; set; }

        /// <summary>
        /// The servers that the user is a member of.
        /// </summary>
        /// <value>The server list.</value>
        public ICollection<Server> Servers { get; set; }
    }
}