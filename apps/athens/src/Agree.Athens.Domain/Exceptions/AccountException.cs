using System.Collections.Generic;

namespace Agree.Athens.Domain.Exceptions
{
    public class AccountException : BaseDomainException
    {
        private AccountException(string message) : base(message)
        {
        }

        public static AccountException EmailAlreadyInUse(string email)
            => new AccountException($"Email {email} is already in use");
        public static AccountException UserTagAlreadyInUse(string tag)
            => new AccountException($"Tag {tag} is already in use by another user with same username");
        public static AccountException RegisterError(IEnumerable<string> errors)
            => new AccountException($"{string.Join("; ", errors)}");
        public static AccountException LoginError()
            => new AccountException($"Wrong credentials on login");
        public static AccountException CannotRevokeRefreshToken()
            => new AccountException($"Cannot revoke refresh token");
        public static AccountException NoRefreshTokens()
            => new AccountException($"Account has no available refresh tokens");
    }
}