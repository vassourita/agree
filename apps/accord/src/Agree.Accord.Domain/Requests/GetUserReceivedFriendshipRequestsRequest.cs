namespace Agree.Accord.Domain.Social.Requests;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Social;
using MediatR;

public class GetUserReceivedFriendshipRequestsRequest : IRequest<IEnumerable<Friendship>>
{
    public GetUserReceivedFriendshipRequestsRequest() { }
    public GetUserReceivedFriendshipRequestsRequest(User user) => User = user;

    [Required]
    public User User { get; set; }
}