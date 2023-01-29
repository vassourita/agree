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
/// Handles the sending of a friendship request.
/// </summary>
public class SendFriendshipRequestHandler : IRequestHandler<SendFriendshipRequestRequest, FriendshipRequestResult>
{
    private readonly IRepository<Friendship, string> _friendshipRepository;
    private readonly AllowClient _allowClient;
    private readonly IMediator _mediator;

    public SendFriendshipRequestHandler(IRepository<Friendship, string> friendshipRepository, AllowClient allowClient, IMediator mediator)
    {
        _friendshipRepository = friendshipRepository;
        _allowClient = allowClient;
        _mediator = mediator;
    }

    public async Task<FriendshipRequestResult> Handle(SendFriendshipRequestRequest request, CancellationToken cancellationToken)
    {
        var validationResult = AnnotationValidator.TryValidate(request);

        if (validationResult.Failed)
        {
            return request.From.NameTag == request.ToNameTag
                ? FriendshipRequestResult.Fail(validationResult.Error.ToErrorList().AddError("ToNameTag", "Cannot send a friendship request to yourself."))
                : FriendshipRequestResult.Fail(validationResult.Error.ToErrorList());
        }

        if (request.From.NameTag == request.ToNameTag)
        {
            return FriendshipRequestResult.Fail(new ErrorList("ToNameTag", "Cannot send a friendship request to yourself."));
        }

        var toUser = await _allowClient.GetUserAccountByNameTag(request.ToNameTag);
        if (toUser == null)
        {
            return FriendshipRequestResult.Fail(new ErrorList("ToNameTag", "User does not exists."));
        }

        var isAlreadyFriend = await _friendshipRepository.GetFirstAsync(new FriendshipExistsSpecification(request.From.Id, toUser.Id));
        if (isAlreadyFriend != null)
        {
            var errorMessage = isAlreadyFriend.Accepted
                ? "You are already friends with this user"
                : isAlreadyFriend.FromId == request.From.Id
                    ? "You have already sent a friendship request to this user."
                    : "The user has already sent a friendship request to you.";
            return FriendshipRequestResult.Fail(new ErrorList("Friendship", errorMessage));
        }

        var friendshipRequest = new Friendship(request.From, toUser);

        await _friendshipRepository.InsertAsync(friendshipRequest);
        await _friendshipRepository.CommitAsync();

        await _mediator.Publish(new FriendshipRequestCreatedNotification(friendshipRequest), cancellationToken);

        return FriendshipRequestResult.Ok(friendshipRequest);
    }
}