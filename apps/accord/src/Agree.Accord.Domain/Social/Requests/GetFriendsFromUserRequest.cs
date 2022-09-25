namespace Agree.Accord.Domain.Social.Requests;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using MediatR;

public class GetFriendsFromUserRequest : IRequest<IEnumerable<UserAccount>>
{
    public GetFriendsFromUserRequest(UserAccount user) => User = user;

    [Required]
    public UserAccount User { get; set; }
}