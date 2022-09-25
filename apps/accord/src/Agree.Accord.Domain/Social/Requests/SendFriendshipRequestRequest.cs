namespace Agree.Accord.Domain.Social.Requests;

using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The request to send a friendship request.
/// </summary>
public class SendFriendshipRequestRequest : IRequest<FriendshipRequestResult>
{
    public SendFriendshipRequestRequest() { }

    /// <summary>
    /// The user sending the friendship request.
    /// </summary>
    /// <value>The user account.</value>
    [Required]
    public UserAccount From { get; set; }

    /// <summary>
    /// The user receiving the friendship request nametag.
    /// Must be a valid user nametag.
    /// </summary>
    /// <value>The receiver user account nametag.</value>
    [Required]
    [RegularExpression(@"^([a-zA-Z0-9_-]{0,}#[0-9]{4})$")]
    public string ToNameTag { get; set; }
}