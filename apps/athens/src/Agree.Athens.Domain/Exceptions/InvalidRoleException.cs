using Agree.Athens.Domain.Entities;

namespace Agree.Athens.Domain.Exceptions
{
    public class InvalidRoleException : BaseDomainException
    {
        private InvalidRoleException(string message) : base(message)
        {
        }

        public static InvalidRoleException RoleIsNotFromServer(Role role, Server server)
            => new InvalidRoleException($"Role {role.Id} does not belongs to server {server.Id}");

        public static InvalidRoleException RoleAlreadyExists(Role role, Server server)
            => new InvalidRoleException($"A role called {role.Name} already exists on server {server.Id}");

        public static InvalidRoleException UserAlreadyHasRole(Role role, User user)
            => new InvalidRoleException($"User {user.Id} already has role {role.Name}");
    }
}