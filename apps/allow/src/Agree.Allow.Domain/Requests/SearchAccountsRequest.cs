namespace Agree.Allow.Domain.Requests;

using System.Collections.Generic;
using Agree.SharedKernel.Data;
using MediatR;

public class SearchAccountsRequest : Pagination, IRequest<IEnumerable<UserAccount>>
{
    public string Query { get; set; }
}