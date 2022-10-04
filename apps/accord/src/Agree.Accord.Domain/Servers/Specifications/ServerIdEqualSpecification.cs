namespace Agree.Accord.Domain.Servers.Specifications;

using System;
using Agree.Accord.Domain.Servers;
using Agree.Accord.SharedKernel.Data;

/// <summary>
/// A specification that checks if the server id of a given server is equal to a given value.
/// </summary>
public class ServerIdEqualSpecification : BaseServerSpecification
{
    public ServerIdEqualSpecification(Guid serverId, Guid userId)
        : base((s) => s.Id == serverId, userId)
    { }
}