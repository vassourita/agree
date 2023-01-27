namespace Agree.Allow.Presentation.Exceptions;

using System;
using System.Runtime.Serialization;
using Agree.SharedKernel;

[Serializable]
public class EmptyBodyException : Exception
{
    private readonly ErrorList _errors = new();

    public EmptyBodyException(string propertyName)
        : base($"{propertyName} cannot be null or empty.")
    {
        _errors.AddError(propertyName, $"{propertyName} cannot be null or empty.");
    }

    public EmptyBodyException(string propertyName, string message)
        : base(message)
    {
        _errors.AddError(propertyName, message);
    }

    protected EmptyBodyException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}