namespace Agree.Accord.Domain.Social.Handlers.Queries;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the retrieval of a direct message by its Id.
/// </summary>
public class GetFriendChatHandler : IRequestHandler<GetFriendChatRequest, IEnumerable<DirectMessage>>
{
    private readonly IDirectMessageRepository _directMessageRepository;

    public GetFriendChatHandler(IDirectMessageRepository directMessageRepository) => _directMessageRepository = directMessageRepository;

    public async Task<IEnumerable<DirectMessage>> Handle(GetFriendChatRequest request, CancellationToken cancellationToken)
        => await _directMessageRepository.SearchAsync(request);
}