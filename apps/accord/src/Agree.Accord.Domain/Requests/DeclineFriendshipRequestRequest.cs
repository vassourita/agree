namespace Agree.Accord.Domain.Social.Requests;

using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Agree.Accord.Domain.Social.Results;

public class DeclineFriendshipRequestRequest : IRequest<FriendshipRequestResult>
{
    public DeclineFriendshipRequestRequest() { }

    public DeclineFriendshipRequestRequest(User loggedUser, Guid friendshipRequestId)
    {
        LoggedUser = loggedUser;
        FriendshipRequestId = friendshipRequestId;
    }

    [Required]
    public User LoggedUser { get; set; }

    [Required]
    public Guid FriendshipRequestId { get; set; }
}