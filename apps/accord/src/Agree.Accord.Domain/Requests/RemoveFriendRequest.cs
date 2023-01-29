namespace Agree.Accord.Domain.Social.Requests;

using Agree.Accord.Domain.Social.Results;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

public class RemoveFriendRequest : IRequest<RemoveFriendResult>
{
    public RemoveFriendRequest() { }

    public RemoveFriendRequest(User user, Guid friendId)
    {
        User = user;
        FriendId = friendId;
    }

    [Required]
    public User User { get; set; }

    [Required]
    public Guid FriendId { get; set; }
}