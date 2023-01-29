namespace Agree.Accord.Domain.Social.Requests;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Social;
using MediatR;

public class GetUserSentFriendshipRequestsRequest : IRequest<IEnumerable<Friendship>>
{
    public GetUserSentFriendshipRequestsRequest() { }
    public GetUserSentFriendshipRequestsRequest(User user) => User = user;

    [Required]
    public User User { get; set; }
}