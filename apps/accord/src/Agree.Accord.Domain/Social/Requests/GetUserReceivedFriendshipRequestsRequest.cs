namespace Agree.Accord.Domain.Social.Requests;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social;
using MediatR;

/// <summary>
/// The request to get a user's received friendship requests.
/// </summary>
public class GetUserReceivedFriendshipRequestsRequest : IRequest<IEnumerable<Friendship>>
{
    public GetUserReceivedFriendshipRequestsRequest() { }
    public GetUserReceivedFriendshipRequestsRequest(UserAccount user) => User = user;

    /// <summary>
    /// The user whose received friendship requests are to be retrieved.
    /// </summary>
    /// <value>The user account.</value>
    [Required]
    public UserAccount User { get; set; }
}