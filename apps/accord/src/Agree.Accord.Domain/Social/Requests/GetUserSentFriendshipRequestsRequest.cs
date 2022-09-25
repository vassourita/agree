namespace Agree.Accord.Domain.Social.Requests;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social;
using MediatR;

/// <summary>
/// The request to get a user's sent friendship requests.
/// </summary>
public class GetUserSentFriendshipRequestsRequest : IRequest<IEnumerable<Friendship>>
{
    public GetUserSentFriendshipRequestsRequest() { }
    public GetUserSentFriendshipRequestsRequest(UserAccount user) => User = user;

    /// <summary>
    /// The user whose sent friendship requests are to be retrieved.
    /// </summary>
    /// <value>The user account.</value>
    [Required]
    public UserAccount User { get; set; }
}