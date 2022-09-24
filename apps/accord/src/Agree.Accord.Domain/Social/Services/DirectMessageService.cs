namespace Agree.Accord.Domain.Social.Services;

using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Social.Dtos;
using Agree.Accord.Domain.Social.Results;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;

public class DirectMessageService
{
    private readonly IRepository<Friendship> _friendshipRepository;
    private readonly IRepository<DirectMessage> _directMessageRepository;
    private readonly AccountService _accountService;

    public DirectMessageService(AccountService accountService, IRepository<Friendship> friendshipRepository, IRepository<DirectMessage> directMessageRepository)
    {
        _accountService = accountService;
        _friendshipRepository = friendshipRepository;
        _directMessageRepository = directMessageRepository;
    }

    public async Task<DirectMessageResult> SendDirectMessageAsync(SendDirectMessageDto sendDirectMessageDto)
    {
        var toUser = await _accountService.GetAccountByIdAsync(sendDirectMessageDto.ToId);
        if (toUser == null)
        {
            return DirectMessageResult.Fail(new ErrorList().AddError("ToId", "User not found"));
        }

        var directMessage = new DirectMessage(sendDirectMessageDto.MessageText, sendDirectMessageDto.From, toUser);

        await _directMessageRepository.InsertAsync(directMessage);
        await _directMessageRepository.CommitAsync();

        return DirectMessageResult.Ok(directMessage);
    }

    public async Task<DirectMessage> GetDirectMessageByIdAsync(Guid id)
        => await _directMessageRepository.GetFirstAsync(new DirectMessageIdEqualSpecification(id));

    public async Task<DirectMessageResult> MarkAsRead(ApplicationUser loggedUser, Guid directMessageId)
    {
        var directMessage = await _directMessageRepository.GetFirstAsync(new DirectMessageIdEqualSpecification(directMessageId));
        if (directMessage == null)
        {
            return DirectMessageResult.Fail(new ErrorList().AddError("DirectMessageId", "Direct message not found."));
        }
        if (directMessage.From.Id != loggedUser.Id)
        {
            return DirectMessageResult.Fail(new ErrorList().AddError("DirectMessageId", "Direct message was not sent to you."));
        }

        directMessage.MarkRead();

        await _directMessageRepository.UpdateAsync(directMessage);
        await _directMessageRepository.CommitAsync();

        return DirectMessageResult.Ok(directMessage);
    }

    public async Task<DirectMessagesReadResult> MarkEntireChatAsRead(Guid requesterId, Guid friendId)
    {
        var directMessages = await _directMessageRepository.GetAllAsync(new DirectMessageFromOrToFriendSpecification(requesterId, friendId));

        var toBeReturned = new List<DirectMessage>();

        foreach (var directMessage in directMessages)
        {
            if (directMessage.From.Id == requesterId)
            {
                continue;
            }
            directMessage.MarkRead();
            toBeReturned.Add(directMessage);
            await _directMessageRepository.UpdateAsync(directMessage);
        }

        await _directMessageRepository.CommitAsync();

        return DirectMessagesReadResult.Ok(toBeReturned);
    }

    public async Task<IEnumerable<DirectMessage>> GetDirectMessagesFromFriendChatAsync(Guid requesterId, Guid friendId, IPagination pagination)
        => await _directMessageRepository.GetAllAsync(new DirectMessageFromOrToFriendPaginatedSpecification(requesterId, friendId, pagination));
}