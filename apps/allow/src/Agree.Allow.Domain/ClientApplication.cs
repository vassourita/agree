namespace Agree.Allow.Domain;

using Agree.SharedKernel;

/// <summary>
/// Represents a registered client application that will access Allow API.
/// </summary>
public class ClientApplication : IEntity<int>
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string AudienceName { get; private set; }
}