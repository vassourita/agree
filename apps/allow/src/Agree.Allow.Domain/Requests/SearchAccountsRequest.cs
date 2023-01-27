namespace Agree.Allow.Domain.Requests;

using System.Collections.Generic;
using Agree.SharedKernel.Data;
using MediatR;

/// <summary>
/// Represents a request to search paginated accounts by a query.
/// </summary>
public class SearchAccountsRequest : Pagination, IRequest<IEnumerable<UserAccount>>
{
    public string Query { get; set; }
}