namespace Agree.Accord.Domain.Social.Handlers.Queries;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Domain.Social.Specifications;
using Agree.Accord.SharedKernel.Data;
using MediatR;

public class GetUserSentFriendshipRequestsHandler : IRequestHandler<GetUserSentFriendshipRequestsRequest, IEnumerable<Friendship>>
{
    private readonly IRepository<Friendship, string> _friendshipRepository;

    public GetUserSentFriendshipRequestsHandler(IRepository<Friendship, string> friendshipRepository) => _friendshipRepository = friendshipRepository;

    public async Task<IEnumerable<Friendship>> Handle(GetUserSentFriendshipRequestsRequest request, CancellationToken cancellationToken)
        => await _friendshipRepository.GetAllAsync(new SentFriendshipRequestSpecification(request.User.Id));
}