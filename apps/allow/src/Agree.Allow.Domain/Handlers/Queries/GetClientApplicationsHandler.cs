namespace Agree.Allow.Domain.Handlers.Queries;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Agree.Allow.Domain;
using Agree.Allow.Domain.Requests;
using Agree.Allow.Domain.Specifications;
using Agree.SharedKernel.Data;
using MediatR;

public class GetClientApplicationsHandlers : IRequestHandler<GetClientApplicationsRequest, IEnumerable<ClientApplication>>
{
    private readonly IRepository<ClientApplication, int> _clientApplicationRepository;

    public GetClientApplicationsHandlers(IRepository<ClientApplication, int> clientApplicationRepository)
        => _clientApplicationRepository = clientApplicationRepository;

    public async Task<IEnumerable<ClientApplication>> Handle(GetClientApplicationsRequest _, CancellationToken cancellationToken)
        => await _clientApplicationRepository.GetAllAsync();
}