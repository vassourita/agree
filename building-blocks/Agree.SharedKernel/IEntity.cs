namespace Agree.SharedKernel;

/// <summary>
/// Represents an entity.
/// </summary>
public interface IEntity<TId>
{
    /// <summary>
    /// Gets the id of the entity.
    /// </summary>
    TId Id { get; }
}