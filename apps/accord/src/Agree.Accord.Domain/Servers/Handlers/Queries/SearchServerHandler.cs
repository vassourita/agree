namespace Agree.Accord.Domain.Servers.Handlers.Queries;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Servers;
using Agree.Accord.Domain.Servers.Requests;
using Agree.Accord.Domain.Servers.Specifications;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the search of paginated <see cref="Server"/>s by a query.
/// </summary>
public class SearchServersHandler : IRequestHandler<SearchServersRequest, IEnumerable<Server>>
{
    private readonly IRepository<Server, Guid> _serverRepository;

    public SearchServersHandler(IRepository<Server, Guid> serverRepository) => _serverRepository = serverRepository;

    public async Task<IEnumerable<Server>> Handle(SearchServersRequest request, CancellationToken cancellationToken)
        => await _serverRepository.GetAllAsync(new ServerInfoLikeSpecification(request.Query, request.UserId, request));
}