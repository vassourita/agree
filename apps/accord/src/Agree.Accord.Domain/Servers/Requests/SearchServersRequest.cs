namespace Agree.Accord.Domain.Servers.Requests;

using System;
using System.Collections.Generic;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Represents a request to search paginated server by a query, taking into account the server's privacy settings.
/// </summary>
public class SearchServersRequest : Pagination, IRequest<IEnumerable<Server>>
{
    /// <summary>
    /// The query to search for. This will be used to search for the server name or description.
    /// </summary>
    /// <value>The query to search for.</value>
    public string Query { get; set; }

    /// <summary>
    /// The id of the user that is searching.
    /// Useful to filter out secret servers.
    /// </summary>
    /// <value>The id of the user that is searching.</value>
    public Guid UserId { get; set; }
}