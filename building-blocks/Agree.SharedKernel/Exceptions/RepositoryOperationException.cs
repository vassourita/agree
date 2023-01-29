namespace Agree.SharedKernel.Exceptions;

using System;

[Serializable]
public class RepositoryOperationException : AgreeException
{
    public RepositoryOperationException(string propertyName, string message, Exception innerException)
        : base(propertyName, message, innerException)
    {
    }
}