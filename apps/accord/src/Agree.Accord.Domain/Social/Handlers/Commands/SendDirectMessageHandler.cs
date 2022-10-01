namespace Agree.Accord.Domain.Social.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Specifications;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Domain.Social.Results;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the sending of a direct message.
/// </summary>
public class SendDirectMessageHandler : IRequestHandler<SendDirectMessageRequest, DirectMessageResult>
{
    private readonly IRepository<DirectMessage, Guid> _directMessageRepository;
    private readonly IUserAccountRepository _accountRepository;

    public SendDirectMessageHandler(IRepository<DirectMessage, Guid> directMessageRepository, IUserAccountRepository userAccountRepository)
    {
        _directMessageRepository = directMessageRepository;
        _accountRepository = userAccountRepository;
    }

    public async Task<DirectMessageResult> Handle(SendDirectMessageRequest request, CancellationToken cancellationToken)
    {
        var toUser = await _accountRepository.GetFirstAsync(new UserIdEqualSpecification(request.ToId));
        if (toUser == null)
        {
            return DirectMessageResult.Fail(new ErrorList().AddError("ToId", "User not found"));
        }
        if (toUser.Id == request.From.Id)
        {
            return DirectMessageResult.Fail(new ErrorList().AddError("ToId", "Cannot send a direct message to yourself"));
        }

        var directMessage = new DirectMessage(request.MessageText, request.From, toUser);

        await _directMessageRepository.InsertAsync(directMessage);
        await _directMessageRepository.CommitAsync();

        return DirectMessageResult.Ok(directMessage);
    }
}