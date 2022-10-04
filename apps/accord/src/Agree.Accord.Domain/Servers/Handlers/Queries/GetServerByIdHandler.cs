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
/// Handles the search of a <see cref="Server"/> by its id.
/// </summary>
public class GetServerByIdHandler : IRequestHandler<GetServerByIdRequest, Server>
{
    private readonly IRepository<Server, Guid> _serverRepository;

    public GetServerByIdHandler(IRepository<Server, Guid> accountRepository) => _serverRepository = accountRepository;

    public async Task<Server> Handle(GetServerByIdRequest request, CancellationToken cancellationToken)
        => await _serverRepository.GetFirstAsync(new ServerIdEqualSpecification(request.ServerId, request.UserId));
}