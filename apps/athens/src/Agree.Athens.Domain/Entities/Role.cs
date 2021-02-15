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
            ServerUsers = new Collection<ServerUser>();
            ServerUserRoles = new List<ServerUserRole>();

            if (name == null) throw new ArgumentNullException(nameof(name));
            if (colorHex == null) colorHex = CreateRandomHexColor();

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
            ServerUsers = new Collection<ServerUser>();
            ServerUserRoles = new List<ServerUserRole>();
        }

        public static Role CreateDefaultOwnerRole(Server server)
        {
            return new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                ColorHex = CreateRandomHexColor(),
                Order = 1,
                CanCreateNewRoles = true,
                CanDeleteRoles = true,
                CanDeleteServer = true,
                CanRemoveUsers = true,
                CanUpdateServerAvatar = true,
                CanUpdateServerDescription = true,
                CanUpdateServerName = true,
                ServerId = server.Id
            };
        }

        public static string CreateRandomHexColor()
        {
            var random = new Random();
            var color = String.Format("{0:X6}", random.Next(0x1000000));
            return color;
        }

        public string Name { get; set; }
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

        public List<ServerUserRole> ServerUserRoles { get; private set; }
        public ICollection<ServerUser> ServerUsers { get; private set; }
    }
}