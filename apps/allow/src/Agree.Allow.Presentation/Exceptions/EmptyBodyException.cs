namespace Agree.Allow.Presentation.Exceptions;

using System;
using System.Runtime.Serialization;
using Agree.SharedKernel;

[Serializable]
public class EmptyBodyException : Exception
{
    public readonly ErrorList Errors = new();

    public EmptyBodyException(string propertyName)
        : base($"{propertyName} cannot be null or empty.")
    {
        Errors.AddError(propertyName, $"{propertyName} cannot be null or empty.");
    }

    public EmptyBodyException(string propertyName, string message)
        : base(message)
    {
        Errors.AddError(propertyName, message);
    }

    protected EmptyBodyException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}