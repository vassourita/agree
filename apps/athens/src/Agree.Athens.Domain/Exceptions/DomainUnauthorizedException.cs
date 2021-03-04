using System;

namespace Agree.Athens.Domain.Exceptions
{
    public class DomainUnauthorizedException : BaseDomainException
    {
        private DomainUnauthorizedException(string message) : base(message)
        {
        }

        public static DomainUnauthorizedException InvalidLogin()
            => new("Email or password are incorrect");

        public static DomainUnauthorizedException InvalidPassword()
            => new("Password is incorrect");

        public static DomainUnauthorizedException AccountNotVerified()
            => new("Account is not verified yet");

        public static DomainUnauthorizedException InvalidRefreshToken()
            => new("Refresh token is invalid");

        public static DomainUnauthorizedException ExpiredRefreshToken()
            => new("Refresh token is expired");

        public static DomainUnauthorizedException AccountDisabled()
            => new("This account is disabled");

        public static DomainUnauthorizedException NoPermission()
            => new("You do not have permission to execute this action");
    }
}