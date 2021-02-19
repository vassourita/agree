using System;
using Agree.Athens.Domain.Entities;

namespace Agree.Athens.Domain.Factories
{
    public static class RoleFactory
    {
        public static Role CreateDefaultOwnerRole(Server server)
        {
            return new Role("Admin", CreateRandomHexColor())
            {
                Order = 1,
                CanCreateNewRoles = true,
                CanDeleteRoles = true,
                CanDeleteServer = true,
                CanRemoveUsers = true,
                CanUpdateServerAvatar = true,
                CanUpdateServerDescription = true,
                CanUpdateServerName = true,
                ServerId = server.Id,
                Server = server
            };
        }

        private static string CreateRandomHexColor()
        {
            var random = new Random();
            var color = string.Format("{0:X6}", random.Next(0x1000000));
            return color;
        }
    }
}