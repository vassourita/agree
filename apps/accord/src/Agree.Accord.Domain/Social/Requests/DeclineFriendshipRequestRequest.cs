namespace Agree.Accord.Domain.Social.Requests;

using System;
using Agree.Accord.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Agree.Accord.Domain.Social.Results;

/// <summary>
/// The request to decline a friendship request.
/// </summary>
public class DeclineFriendshipRequestRequest : IRequest<FriendshipRequestResult>
{
    public DeclineFriendshipRequestRequest() { }

    public DeclineFriendshipRequestRequest(UserAccount loggedUser, Guid fromUserId)
    {
        LoggedUser = loggedUser;
        FromUserId = fromUserId;
    }

    /// <summary>
    /// The user declining the friendship request.
    /// </summary>
    /// <value>The user account.</value>
    [Required]
    public UserAccount LoggedUser { get; set; }

    /// <summary>
    /// The id of the friendship request sender.
    /// </summary>
    /// <value>The sender account id.</value>
    [Required]
    public Guid FromUserId { get; set; }
}