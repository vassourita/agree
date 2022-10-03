namespace Agree.Accord.Domain.Servers.Handlers.Queries;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Servers;
using Agree.Accord.Domain.Servers.Requests;
using MediatR;

/// <summary>
/// Handles the search of paginated <see cref="Server"/>s by a query.
public class SearchServersHandler : IRequestHandler<SearchServersRequest, IEnumerable<Server>>
{
    private readonly IServerRepository _serverRepository;

    public SearchServersHandler(IServerRepository accountRepository) => _serverRepository = accountRepository;

    public async Task<IEnumerable<Server>> Handle(SearchServersRequest request, CancellationToken cancellationToken)
        => await _serverRepository.SearchAsync(request.Query, request.UserId, request);
}