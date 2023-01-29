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

public class GetClientApplicationByAccessKeyHandler : IRequestHandler<GetClientApplicationByAccessKeyRequest, ClientApplication>
{
    private readonly IRepository<ClientApplication, Guid> _clientApplicationRepository;

    public GetClientApplicationByAccessKeyHandler(IRepository<ClientApplication, Guid> clientApplicationRepository)
        => _clientApplicationRepository = clientApplicationRepository;

    public async Task<ClientApplication> Handle(GetClientApplicationByAccessKeyRequest request, CancellationToken cancellationToken)
        => await _clientApplicationRepository.GetFirstAsync(new ClientApplicationAccessKeyEqualSpecification(request.AccessKey));
}