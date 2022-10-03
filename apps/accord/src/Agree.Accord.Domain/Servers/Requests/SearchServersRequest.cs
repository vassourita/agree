namespace Agree.Accord.Domain.Servers.Requests;

using System.Collections.Generic;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Represents a request to search paginated server by a query.
/// </summary>
public class SearchServersRequest : Pagination, IRequest<IEnumerable<Server>>
{
    public string Query { get; set; }
}