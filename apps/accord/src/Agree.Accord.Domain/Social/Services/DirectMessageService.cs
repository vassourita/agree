using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Social.Dtos;
using Agree.Accord.Domain.Social.Results;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Social.Services
{
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

        public async Task<DirectMessageResult> SendDirectMessage(SendDirectMessageDto sendDirectMessageDto)
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
    }
}