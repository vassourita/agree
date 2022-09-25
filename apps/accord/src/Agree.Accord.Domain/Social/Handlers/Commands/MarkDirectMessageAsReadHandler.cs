namespace Agree.Accord.Domain.Social.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Domain.Social.Results;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the marking of a direct message as read.
/// </summary>
public class MarkDirectMessageAsReadHandler : IRequestHandler<MarkDirectMessageAsReadRequest, DirectMessageResult>
{
    private readonly IRepository<DirectMessage, Guid> _directMessageRepository;

    public MarkDirectMessageAsReadHandler(IRepository<DirectMessage, Guid> directMessageRepository)
        => _directMessageRepository = directMessageRepository;

    public async Task<DirectMessageResult> Handle(MarkDirectMessageAsReadRequest request, CancellationToken cancellationToken)
    {
        var directMessage = await _directMessageRepository.GetFirstAsync(new DirectMessageIdEqualSpecification(request.DirectMessageId));
        if (directMessage == null)
        {
            return DirectMessageResult.Fail(new ErrorList().AddError("DirectMessageId", "Direct message not found."));
        }
        if (directMessage.From.Id != request.Requester.Id)
        {
            return DirectMessageResult.Fail(new ErrorList().AddError("DirectMessageId", "Direct message was not sent to you."));
        }

        directMessage.MarkRead();

        await _directMessageRepository.UpdateAsync(directMessage);
        await _directMessageRepository.CommitAsync();

        return DirectMessageResult.Ok(directMessage);
    }
}