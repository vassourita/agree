using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using Agree.Athens.Domain.Interfaces;
using Agree.Athens.Domain.Exceptions;

namespace Agree.Athens.Domain.Entities
{
    public class Role : BaseEntity, IAggregateRoot
    {
        public Role(string name, string colorHex) : base()
        {
            Users = new Collection<User>();

            if (name == null) throw new ArgumentNullException(nameof(name));
            if (colorHex == null) throw new ArgumentNullException(nameof(colorHex));

            Name = name;
            if (colorHex.Length == 3)
            {
                ColorHex = $"{colorHex[0]}{colorHex[0]}{colorHex[1]}{colorHex[1]}{colorHex[2]}{colorHex[2]}";
            }
            else if (colorHex.Length != 6)
            {
                throw new InvalidColorHexException(colorHex);
            }
            else
            {
                ColorHex = colorHex;
            }
        }

        protected Role()
        {
            Users = new Collection<User>();
        }

        [MinLength(1)]
        [MaxLength(40)]
        public string Name { get; set; }
        [MinLength(6)]
        [MaxLength(6)]
        public string ColorHex { get; set; }
        public int Order { get; set; }

        public bool CanCreateNewRoles { get; set; }
        public bool CanDeleteRoles { get; set; }
        public bool CanDeleteServer { get; set; }
        public bool CanRemoveUsers { get; set; }
        public bool CanUpdateServerAvatar { get; set; }
        public bool CanUpdateServerDescription { get; set; }
        public bool CanUpdateServerName { get; set; }

        public Guid ServerId { get; set; }
        public Server Server { get; set; }

        public ICollection<User> Users { get; private set; }
    }
}