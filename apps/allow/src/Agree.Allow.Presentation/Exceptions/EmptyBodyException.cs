namespace Agree.Allow.Presentation.Exceptions;

using System;
using Agree.SharedKernel.Exceptions;

[Serializable]
public class EmptyBodyException : AgreeException
{
    public EmptyBodyException()
        : base("Body", "Empty request body")
    {
    }
}