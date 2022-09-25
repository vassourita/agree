namespace Agree.Accord.Domain.Social.Handlers.Queries;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the retrieval of a user's friends.
/// </summary>
public class GetFriendsFromUserHandler : IRequestHandler<GetFriendsFromUserRequest, IEnumerable<UserAccount>>
{
    private readonly IRepository<Friendship, string> _friendshipRepository;

    public GetFriendsFromUserHandler(IRepository<Friendship, string> friendshipRepository) => _friendshipRepository = friendshipRepository;

    public async Task<IEnumerable<UserAccount>> Handle(GetFriendsFromUserRequest request, CancellationToken cancellationToken)
    {
        var friends = await _friendshipRepository.GetAllAsync(new FriendshipAcceptedSpecification(request.User.Id));
        return friends.Select(friendship => friendship.ToId == request.User.Id ? friendship.From : friendship.To);
    }
}