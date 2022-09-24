namespace Agree.Accord.Domain.Social.Services;

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

/// <summary>
/// Provides methods for the social domain, managing friendships and other interactions between users.
/// </summary>
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
            return friendshipRequestDto.From.NameTag == friendshipRequestDto.ToNameTag
                ? FriendshipRequestResult.Fail(validationResult.Error.ToErrorList().AddError("ToNameTag", "Cannot send a friendship request to yourself."))
                : FriendshipRequestResult.Fail(validationResult.Error.ToErrorList());
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

    public async Task<IEnumerable<Friendship>> GetReceivedFriendshipRequestsFromUserAsync(ApplicationUser user) => await _friendshipRepository.GetAllAsync(new ReceivedFriendshipRequestSpecification(user.Id));

    public async Task<IEnumerable<Friendship>> GetSentFriendshipRequestsFromUserAsync(ApplicationUser user) => await _friendshipRepository.GetAllAsync(new SentFriendshipRequestSpecification(user.Id));

    public async Task<FriendshipRequestResult> AcceptFriendshipRequestAsync(ApplicationUser user, Guid fromUserId)
    {
        var friendshipRequest = await _friendshipRepository.GetFirstAsync(new FriendshipExistsSpecification(fromUserId, user.Id));
        if (friendshipRequest == null)
            return FriendshipRequestResult.Fail(new ErrorList().AddError("Friendship", "Friendship request does not exist."));
        if (friendshipRequest.Accepted)
            return FriendshipRequestResult.Fail(new ErrorList().AddError("Friendship", "Friendship request already accepted."));

        friendshipRequest.Accept();

        await _friendshipRepository.UpdateAsync(friendshipRequest);
        await _friendshipRepository.CommitAsync();

        return FriendshipRequestResult.Ok(friendshipRequest);
    }

    public async Task<FriendshipRequestResult> DeclineFriendshipRequestAsync(ApplicationUser user, Guid fromUserId)
    {
        var friendshipRequest = await _friendshipRepository.GetFirstAsync(new FriendshipExistsSpecification(fromUserId, user.Id));
        if (friendshipRequest == null)
            return FriendshipRequestResult.Fail(new ErrorList().AddError("Friendship", "Friendship request does not exist."));
        if (friendshipRequest.Accepted)
            return FriendshipRequestResult.Fail(new ErrorList().AddError("Friendship", "Friendship request already accepted."));

        await _friendshipRepository.DeleteAsync(friendshipRequest);
        await _friendshipRepository.CommitAsync();

        return FriendshipRequestResult.Ok(friendshipRequest);
    }

    public async Task<RemoveFriendResult> RemoveFriend(ApplicationUser user, Guid friendId)
    {
        var friendship = await _friendshipRepository.GetFirstAsync(new FriendshipExistsSpecification(user.Id, friendId));

        if (friendship == null)
            return RemoveFriendResult.Fail(new ErrorList().AddError("Friendship", "Friendship does not exist."));
        if (!friendship.Accepted)
            return RemoveFriendResult.Fail(new ErrorList().AddError("Friendship", "You are not friends with this user."));

        await _friendshipRepository.DeleteAsync(friendship);
        await _friendshipRepository.CommitAsync();

        return RemoveFriendResult.Ok(friendship);
    }
}