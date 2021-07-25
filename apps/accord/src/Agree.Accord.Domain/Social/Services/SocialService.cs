using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Specifications;
using Agree.Accord.Domain.Social.Dtos;
using Agree.Accord.Domain.Social.Results;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Social
{
    public class SocialService
    {
        private readonly IRepository<Friendship> _friendshipRepository;
        private readonly IRepository<ApplicationUser> _accountRepository;

        public SocialService(IRepository<ApplicationUser> accountRepository, IRepository<Friendship> friendshipRepository)
        {
            _accountRepository = accountRepository;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<FriendshipRequestResult> SendFriendshipRequest(SendFriendshipRequestDto friendshipRequestDto)
        {
            var validationResult = AnnotationValidator.TryValidate(friendshipRequestDto);

            if (validationResult.Failed)
            {
                if (friendshipRequestDto.From.NameTag == friendshipRequestDto.ToNameTag)
                {
                    return FriendshipRequestResult.Fail(validationResult.Error.ToErrorList().AddError("ToNameTag", "Cannot send a friendship request to yourself."));
                }
                return FriendshipRequestResult.Fail(validationResult.Error.ToErrorList());
            }

            if (friendshipRequestDto.From.NameTag == friendshipRequestDto.ToNameTag)
            {
                return FriendshipRequestResult.Fail(new ErrorList().AddError("ToNameTag", "Cannot send a friendship request to yourself."));
            }

            var nameTag = friendshipRequestDto.ToNameTag.Split('#');
            var displayName = nameTag[0];
            var tag = DiscriminatorTag.Parse(nameTag[1]);
            var toUser = await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, displayName));
            if (toUser == null)
            {
                return FriendshipRequestResult.Fail(new ErrorList().AddError("ToNameTag", "User does not exists."));
            }

            var isAlreadyFriend = await _friendshipRepository.GetFirstAsync(new FriendshipExistsSpecification(friendshipRequestDto.From.Id, toUser.Id));
            if (isAlreadyFriend != null)
            {
                var errorMessage = isAlreadyFriend.Accepted
                    ? "You are already friends with this user"
                    : isAlreadyFriend.FromId == friendshipRequestDto.From.Id
                        ? "You have already sent a friendship request to this user."
                        : "The user has already sent a friendship request to you.";
                return FriendshipRequestResult.Fail(new ErrorList().AddError("Friendship", errorMessage));
            }

            var friendship = new Friendship(friendshipRequestDto.From, toUser);

            await _friendshipRepository.InsertAsync(friendship);
            await _friendshipRepository.CommitAsync();

            return FriendshipRequestResult.Ok(friendship);
        }

        public async Task<IEnumerable<ApplicationUser>> GetFriendsFromUserAsync(ApplicationUser user)
        {
            var friends = await _friendshipRepository.GetAllAsync(new FriendshipAcceptedSpecification(user.Id));
            return friends.Select(friendship => friendship.ToId == user.Id ? friendship.From : friendship.To);
        }

        public async Task<IEnumerable<Friendship>> GetReceivedFriendshipRequestsFromUserAsync(ApplicationUser user)
        {
            return await _friendshipRepository.GetAllAsync(new ReceivedFriendshipRequestSpecification(user.Id));
        }

        public async Task<IEnumerable<Friendship>> GetSentFriendshipRequestsFromUserAsync(ApplicationUser user)
        {
            return await _friendshipRepository.GetAllAsync(new SentFriendshipRequestSpecification(user.Id));
        }

        public async Task<bool> AcceptFriendshipRequestAsync(AcceptFriendshipRequestDto acceptFriendshipRequestDto)
        {
            var friendshipRequest = await _friendshipRepository.GetFirstAsync(new FriendshipExistsSpecification(acceptFriendshipRequestDto.FromUserId, acceptFriendshipRequestDto.LoggedUser.Id));
            if (friendshipRequest == null) return false;
            if (friendshipRequest.Accepted) return false;

            friendshipRequest.Accept();

            await _friendshipRepository.UpdateAsync(friendshipRequest);
            await _friendshipRepository.CommitAsync();

            return true;
        }

        public async Task<bool> DeclineFriendshipRequestAsync(ApplicationUser user, Guid fromUserId)
        {
            var friendshipRequest = await _friendshipRepository.GetFirstAsync(new FriendshipExistsSpecification(fromUserId, user.Id));
            if (friendshipRequest == null) return false;
            if (friendshipRequest.Accepted) return false;

            await _friendshipRepository.DeleteAsync(friendshipRequest);
            await _friendshipRepository.CommitAsync();

            return true;
        }
    }
}