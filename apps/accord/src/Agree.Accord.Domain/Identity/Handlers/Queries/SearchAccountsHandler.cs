namespace Agree.Accord.Domain.Identity.Handlers.Queries;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Requests;
using MediatR;

/// <summary>
/// Handles the search of paginated <see cref="UserAccount"/>s by a query.
public class SearchAccountsHandler : IRequestHandler<SearchAccountsRequest, IEnumerable<UserAccount>>
{
    private readonly IUserAccountRepository _accountRepository;

    public SearchAccountsHandler(IUserAccountRepository accountRepository) => _accountRepository = accountRepository;

    public async Task<IEnumerable<UserAccount>> Handle(SearchAccountsRequest request, CancellationToken cancellationToken)
        => await _accountRepository.SearchAsync(request.Query, request);
}