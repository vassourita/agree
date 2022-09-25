namespace Agree.Accord.Domain.Social.Requests;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social;
using MediatR;

public class GetUserReceivedFriendshipRequestsRequest : IRequest<IEnumerable<Friendship>>
{
    public GetUserReceivedFriendshipRequestsRequest(UserAccount user) => User = user;

    [Required]
    public UserAccount User { get; set; }
}