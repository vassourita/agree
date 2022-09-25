namespace Agree.Accord.Domain.Social.Requests;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using MediatR;

/// <summary>
/// The request to get a user's friends.
/// </summary>
public class GetFriendsFromUserRequest : IRequest<IEnumerable<UserAccount>>
{
    public GetFriendsFromUserRequest() { }
    public GetFriendsFromUserRequest(UserAccount user) => User = user;

    /// <summary>
    /// The user whose friends are to be retrieved.
    /// </summary>
    /// <value>The user account.</value>
    [Required]
    public UserAccount User { get; set; }
}