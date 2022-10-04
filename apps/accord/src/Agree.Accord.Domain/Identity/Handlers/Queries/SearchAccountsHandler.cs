namespace Agree.Accord.Domain.Identity.Handlers.Queries;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Requests;
using Agree.Accord.Domain.Identity.Specifications;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the search of paginated <see cref="UserAccount"/>s by a query.
/// </summary>
public class SearchAccountsHandler : IRequestHandler<SearchAccountsRequest, IEnumerable<UserAccount>>
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;

    public SearchAccountsHandler(IRepository<UserAccount, Guid> accountRepository) => _accountRepository = accountRepository;

    public async Task<IEnumerable<UserAccount>> Handle(SearchAccountsRequest request, CancellationToken cancellationToken)
        => await _accountRepository.GetAllAsync(new NameTagLikeSpecification(request.Query, request));
}