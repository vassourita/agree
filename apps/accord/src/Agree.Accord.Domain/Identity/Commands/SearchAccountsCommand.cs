namespace Agree.Accord.Domain.Identity.Commands;

using System.Collections.Generic;
using Agree.Accord.SharedKernel.Data;
using MediatR;

public class SearchAccountsCommand : Pagination, IRequest<IEnumerable<UserAccount>>
{
    public string Query { get; set; }
}