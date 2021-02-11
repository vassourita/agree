using Agree.Athens.Domain.Entities;

namespace Agree.Athens.Domain.Exceptions
{
    public class UnauthorizedUserException : BaseDomainException
    {
        private UnauthorizedUserException(string message) : base(message)
        {
        }

        public static UnauthorizedUserException UserIsNotMemberOfServer(User user, Server server)
            => new UnauthorizedUserException($"User {user.Id} is not member of server {server.Id}");
    }
}