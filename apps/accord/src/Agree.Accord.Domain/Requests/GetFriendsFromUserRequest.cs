namespace Agree.Accord.Domain.Social.Requests;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

public class GetFriendsFromUserRequest : IRequest<IEnumerable<User>>
{
    public GetFriendsFromUserRequest() { }
    public GetFriendsFromUserRequest(User user) => User = user;

    [Required]
    public User User { get; set; }
}