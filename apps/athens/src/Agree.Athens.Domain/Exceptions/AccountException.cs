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
    }
}