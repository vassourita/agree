namespace Agree.SharedKernel.Exceptions;

using System;

[Serializable]
public abstract class DomainException : AgreeException
{
    public DomainException() : base()
    {
    }

    public DomainException(ErrorList errors) : base(errors)
    {
    }

    public DomainException(string propertyName, string message)
        : base(propertyName, message)
    {
    }
}