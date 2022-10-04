namespace Agree.Accord.Domain.Servers.Specifications;

using System;
using Agree.Accord.Domain.Servers;
using Agree.Accord.SharedKernel.Data;

/// <summary>
/// A specification that checks if the server id of a given server is equal to a given value.
/// </summary>
public class ServerInfoLikeSpecification : BaseServerPaginatedSpecification
{
    public ServerInfoLikeSpecification(string query, Guid userId, IPagination pagination)
        : base(
            (s) => s.Name.ToLower().Contains(query.ToLower()) || s.Description.ToLower().Contains(query.ToLower()),
            userId,
            pagination
        )
    { }
}