namespace Agree.Accord.Domain.Social.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social.Notifications;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Domain.Social.Results;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the sending of a direct message.
/// </summary>
public class SendDirectMessageHandler : IRequestHandler<SendDirectMessageRequest, DirectMessageResult>
{
    private readonly IRepository<DirectMessage, Guid> _directMessageRepository;
    private readonly AllowClient _allowClient;
    private readonly IMediator _mediator;

    public SendDirectMessageHandler(IRepository<DirectMessage, Guid> directMessageRepository, AllowClient allowClient, IMediator mediator)
    {
        _directMessageRepository = directMessageRepository;
        _allowClient = allowClient;
        _mediator = mediator;
    }

    public async Task<DirectMessageResult> Handle(SendDirectMessageRequest request, CancellationToken cancellationToken)
    {
        var toUser = await _allowClient.GetUserAccountById(request.ToId);
        if (toUser == null)
            return DirectMessageResult.Fail(new ErrorList("ToId", "User not found"));
        if (toUser.Id == request.From.Id)
            return DirectMessageResult.Fail(new ErrorList("ToId", "Cannot send a direct message to yourself"));

        var inReplyTo = request.InReplyToId.HasValue
            ? await _directMessageRepository.GetFirstAsync(new DirectMessageIdEqualSpecification(request.InReplyToId.Value))
            : null;

        if (inReplyTo != null && inReplyTo.FromId != request.From.Id && inReplyTo.ToId != request.From.Id)
            return DirectMessageResult.Fail(new ErrorList("InReplyToId", "Cannot reply to a message that is not part of the conversation"));

        var directMessage = new DirectMessage(request.MessageText, request.From, toUser, inReplyTo);

        await _directMessageRepository.InsertAsync(directMessage);
        await _directMessageRepository.CommitAsync();

        await _mediator.Publish(new DirectMessageCreatedNotification(directMessage), cancellationToken);

        return DirectMessageResult.Ok(directMessage);
    }
}