namespace Agree.SharedKernel.Exceptions;

using System;
using System.Runtime.Serialization;

[Serializable]
public abstract class AgreeException : Exception
{
    public ErrorList Errors { get; protected set; }

    public AgreeException()
    {
        Errors = new ErrorList();
    }

    public AgreeException(ErrorList errors)
    {
        Errors = errors;
    }

    public AgreeException(string propertyName, string message)
        : base(message)
    {
        Errors = new ErrorList();
        Errors.AddError(propertyName, message);
    }

    public AgreeException(string propertyName, string message, Exception innerException)
        : base(message, innerException)
    {
        Errors = new ErrorList();
        Errors.AddError(propertyName, message);
    }

    protected AgreeException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}