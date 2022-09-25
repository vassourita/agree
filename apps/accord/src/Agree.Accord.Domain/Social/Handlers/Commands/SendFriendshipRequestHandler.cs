namespace Agree.Accord.Domain.Social.Handlers.Commands;

using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Specifications;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Domain.Social.Results;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;

public class SendFriendshipRequestHandler : IRequestHandler<SendFriendshipRequestRequest, FriendshipRequestResult>
{
    private readonly IRepository<Friendship, string> _friendshipRepository;
    private readonly IUserAccountRepository _accountRepository;

    public SendFriendshipRequestHandler(IRepository<Friendship, string> friendshipRepository, IUserAccountRepository userAccountRepository)
    {
        _friendshipRepository = friendshipRepository;
        _accountRepository = userAccountRepository;
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
            return FriendshipRequestResult.Fail(new ErrorList().AddError("ToNameTag", "Cannot send a friendship request to yourself."));
        }

        var nameTag = request.ToNameTag.Split('#');
        var displayName = nameTag[0];
        var tag = DiscriminatorTag.Parse(nameTag[1]);
        var toUser = await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, displayName));
        if (toUser == null)
        {
            return FriendshipRequestResult.Fail(new ErrorList().AddError("ToNameTag", "User does not exists."));
        }

        var isAlreadyFriend = await _friendshipRepository.GetFirstAsync(new FriendshipExistsSpecification(request.From.Id, toUser.Id));
        if (isAlreadyFriend != null)
        {
            var errorMessage = isAlreadyFriend.Accepted
                ? "You are already friends with this user"
                : isAlreadyFriend.FromId == request.From.Id
                    ? "You have already sent a friendship request to this user."
                    : "The user has already sent a friendship request to you.";
            return FriendshipRequestResult.Fail(new ErrorList().AddError("Friendship", errorMessage));
        }

        var friendship = new Friendship(request.From, toUser);

        await _friendshipRepository.InsertAsync(friendship);
        await _friendshipRepository.CommitAsync();

        return FriendshipRequestResult.Ok(friendship);
    }
}