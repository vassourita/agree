namespace Agree.Accord.Domain.Social.Handlers.Queries;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the retrieval of a direct message by its Id.
/// </summary>
public class GetDirectMessageByIdHandler : IRequestHandler<GetDirectMessagebyIdRequest, DirectMessage>
{
    private readonly IDirectMessageRepository _directMessageRepository;

    public GetDirectMessageByIdHandler(IDirectMessageRepository directMessageRepository) => _directMessageRepository = directMessageRepository;

    public async Task<DirectMessage> Handle(GetDirectMessagebyIdRequest request, CancellationToken cancellationToken)
        => await _directMessageRepository.GetFirstAsync(new DirectMessageIdEqualSpecification(request.Id));
}