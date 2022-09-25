namespace Agree.Accord.Domain.Identity.Requests;

using System.Collections.Generic;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Represents a request to search paginated accounts by a query.
/// </summary>
public class SearchAccountsRequest : Pagination, IRequest<IEnumerable<UserAccount>>
{
    public string Query { get; set; }
}