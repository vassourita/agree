namespace Agree.Accord.Domain.Identity.Requests;

using System.Collections.Generic;
using Agree.Accord.SharedKernel.Data;
using MediatR;

public class SearchAccountsRequest : Pagination, IRequest<IEnumerable<UserAccount>>
{
    public string Query { get; set; }
}