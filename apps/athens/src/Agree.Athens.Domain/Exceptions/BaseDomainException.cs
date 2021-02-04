using System;
namespace Agree.Athens.Domain.Exceptions
{
    public abstract class BaseDomainException : Exception
    {
        public BaseDomainException(string message) : base(message)
        { }

    }
}