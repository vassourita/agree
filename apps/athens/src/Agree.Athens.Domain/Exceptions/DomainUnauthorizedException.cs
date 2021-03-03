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

        public static Exception AccountNotVerified()
            => new("Account is not verified yet");
    }
}