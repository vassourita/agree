namespace Agree.Accord.SharedKernel;

using System;
/// <summary>
/// A abstract result, with a flag of whether the operation succeeded or not.
/// </summary>
public interface IResult
{
    /// <summary>
    /// A flag indicating if the operation succeeded.
    /// </summary>
    /// <value><c>true</c> if the operation succeeded; otherwise, <c>false</c></value>
    bool Succeeded { get; }

    /// <summary>
    /// A flag indicating if the operation failed.
    /// </summary>
    /// <value><c>true</c> if the operation failed; otherwise, <c>false</c></value>
    bool Failed { get; }
}

/// <summary>
/// A abstract result, with a flag of whether the operation succeeded or not.
/// </summary>
public abstract class Result : IResult
{
    protected Result(bool succeeded)
        => Succeeded = succeeded;

    /// <summary>
    /// A flag indicating if the operation succeeded.
    /// </summary>
    /// <value><c>true</c> if the operation succeeded; otherwise, <c>false</c></value>
    public bool Succeeded { get; private set; }

    /// <summary>
    /// A flag indicating if the operation failed.
    /// </summary>
    /// <value><c>true</c> if the operation failed; otherwise, <c>false</c></value>
    public bool Failed => !Succeeded;
}

/// <summary>
/// A abstract result, with a flag of whether the operation succeeded or not, and a property for operation data.
/// </summary>
public abstract class Result<TData> : Result, IResult
{
    /// <summary>
    /// A abstract result, with a flag of whether the operation succeeded or not, and a property for operation data.
    /// </summary>
    /// <param name="data">The data of the succeeded operation</param>
    protected Result(TData data) : base(true)
        => Data = data;

    /// <summary>
    /// A abstract result, with a flag of whether the operation succeeded or not, and a property for operation data.
    /// </summary>
    protected Result() : base(false)
    {
    }

    private TData _data;

    /// <summary>
    /// Gets the operation data if it has succeeded.
    /// </summary>
    /// <value>A value of type <c>TData</c> if the operation succeeded.</value>
    /// <exception cref="Exception">Thrown when trying to access the data from a failed result.</exception>
    public TData Data
    {
        get => Succeeded ? _data : throw new Exception($"You can't access .{nameof(Data)} when .{nameof(Succeeded)} is false");
        private set => _data = value;
    }
}

/// <summary>
/// A abstract result, with a flag of whether the operation succeeded or not, a property for operation data, and a property for error data.
/// </summary>
public abstract class Result<TData, TError> : Result, IResult
{
    /// <summary>
    /// A abstract result, with a flag of whether the operation succeeded or not, and a property for operation data.
    /// </summary>
    /// <param name="data">The data of the succeeded operation.</param>
    protected Result(TData data) : base(true)
        => Data = data;

    protected Result(TError error) : base(false)
        => Error = error;

    private TData _data;
    private TError _error;

    /// <summary>
    /// Gets the operation data if it has succeeded.
    /// </summary>
    /// <value>A value of type <c>TData</c> if the operation succeeded.</value>
    /// <exception cref="System.Exception">Thrown when trying to access the data from a failed result.</exception>
    public TData Data
    {
        get => Succeeded ? _data : throw new Exception($"You can't access .{nameof(Data)} when .{nameof(Succeeded)} is false");
        private set => _data = value;
    }

    /// <summary>
    /// Gets the operation error if it has failed.
    /// </summary>
    /// <value>A value of type <c>TError</c> if the operation failed.</value>
    /// <exception cref="System.Exception">Thrown when trying to access the data from a succeeded result.</exception>
    public TError Error
    {
        get => Failed ? _error : throw new Exception($"You can't access .{nameof(Error)} when .{nameof(Failed)} is false");
        private set => _error = value;
    }
}