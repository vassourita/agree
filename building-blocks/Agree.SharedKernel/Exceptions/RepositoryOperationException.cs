namespace Agree.SharedKernel.Exceptions;

using System;
using System.Runtime.Serialization;

/// <summary>
/// Represents an exception that occurs when a repository operation fails.
/// </summary>
[Serializable]
public class RepositoryOperationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryOperationException"/> class.
    /// </summary>
    public RepositoryOperationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryOperationException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public RepositoryOperationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryOperationException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public RepositoryOperationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryOperationException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected RepositoryOperationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}