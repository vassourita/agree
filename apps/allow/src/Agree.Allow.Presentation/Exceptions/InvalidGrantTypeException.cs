namespace Agree.Allow.Presentation.Exceptions;

using System;
using System.Runtime.Serialization;
using Agree.SharedKernel;

[Serializable]
public class InvalidGrantTypeException : Exception
{
    private readonly ErrorList _errors = new();

    public InvalidGrantTypeException(string grantType)
        : base($"Invalid grant type '{grantType}'. Must be 'refresh_token' or 'password'.")
    {
        _errors.AddError("grant_type", $"Invalid grant type '{grantType}'. Must be 'refresh_token' or 'password'.");
    }

    public InvalidGrantTypeException()
        : base("Empty grant type")
    {
        _errors.AddError("grant_type", "Empty grant type. Must be 'refresh_token' or 'password'.");
    }

    protected InvalidGrantTypeException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}