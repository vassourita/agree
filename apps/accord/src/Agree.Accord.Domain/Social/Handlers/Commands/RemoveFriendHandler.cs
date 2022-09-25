namespace Agree.Accord.Domain.Social.Handlers.Commands;

using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Domain.Social.Results;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;

public class RemoveFriendHandler : IRequestHandler<RemoveFriendRequest, RemoveFriendResult>
{
    private readonly IRepository<Friendship, string> _friendshipRepository;

    public RemoveFriendHandler(IRepository<Friendship, string> friendshipRepository) => _friendshipRepository = friendshipRepository;

    public async Task<RemoveFriendResult> Handle(RemoveFriendRequest request, CancellationToken cancellationToken)
    {

        var friendship = await _friendshipRepository.GetFirstAsync(new FriendshipExistsSpecification(request.User.Id, request.FriendId));

        if (friendship == null)
            return RemoveFriendResult.Fail(new ErrorList().AddError("Friendship", "Friendship does not exist."));
        if (!friendship.Accepted)
            return RemoveFriendResult.Fail(new ErrorList().AddError("Friendship", "You are not friends with this user."));

        await _friendshipRepository.DeleteAsync(friendship);
        await _friendshipRepository.CommitAsync();

        return RemoveFriendResult.Ok(friendship);
    }
}