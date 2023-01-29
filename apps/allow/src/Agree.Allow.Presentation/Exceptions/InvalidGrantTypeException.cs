namespace Agree.Allow.Presentation.Exceptions;

using System;
using System.Runtime.Serialization;
using Agree.SharedKernel;
using Agree.SharedKernel.Exceptions;

[Serializable]
public class InvalidGrantTypeException : AgreeException
{
    public InvalidGrantTypeException(string grantType)
        : base("grant_type", $"Invalid grant type '{grantType}'. Must be 'refresh_token' or 'password'.")
    {
    }

    public InvalidGrantTypeException()
        : base("grant_type", "Empty grant type")
    {
    }
}