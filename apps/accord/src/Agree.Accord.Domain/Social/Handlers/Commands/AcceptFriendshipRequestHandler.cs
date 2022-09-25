namespace Agree.Accord.Domain.Social.Handlers.Commands;

using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Domain.Social.Results;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;

public class AcceptFriendshipRequestHandler : IRequestHandler<AcceptFriendshipRequestRequest, FriendshipRequestResult>
{
    private readonly IRepository<Friendship, string> _friendshipRepository;

    public AcceptFriendshipRequestHandler(IRepository<Friendship, string> friendshipRepository) => _friendshipRepository = friendshipRepository;

    public async Task<FriendshipRequestResult> Handle(AcceptFriendshipRequestRequest request, CancellationToken cancellationToken)
    {
        var friendshipRequest = await _friendshipRepository.GetFirstAsync(new FriendshipExistsSpecification(request.FromUserId, request.LoggedUser.Id));
        if (friendshipRequest == null)
            return FriendshipRequestResult.Fail(new ErrorList().AddError("Friendship", "Friendship request does not exist."));
        if (friendshipRequest.Accepted)
            return FriendshipRequestResult.Fail(new ErrorList().AddError("Friendship", "Friendship request already accepted."));

        friendshipRequest.Accept();

        await _friendshipRepository.UpdateAsync(friendshipRequest);
        await _friendshipRepository.CommitAsync();

        return FriendshipRequestResult.Ok(friendshipRequest);
    }
}