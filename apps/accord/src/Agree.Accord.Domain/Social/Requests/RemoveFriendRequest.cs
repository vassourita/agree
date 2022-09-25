namespace Agree.Accord.Domain.Social.Requests;

using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social.Results;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The request to remove a friend.
/// </summary>
public class RemoveFriendRequest : IRequest<RemoveFriendResult>
{
    public RemoveFriendRequest() { }

    public RemoveFriendRequest(UserAccount user, Guid friendId)
    {
        User = user;
        FriendId = friendId;
    }

    /// <summary>
    /// The user removing the friend.
    /// </summary>
    /// <value>The user account.</value>
    [Required]
    public UserAccount User { get; set; }

    /// <summary>
    /// The id of the friend to be removed.
    /// </summary>
    /// <value>The friend account id.</value>
    [Required]
    public Guid FriendId { get; set; }
}