namespace Agree.Accord.Domain.Social.Requests;

using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;

public class SendFriendshipRequestRequest : IRequest<FriendshipRequestResult>
{
    [Required]
    public UserAccount From { get; set; }

    [Required]
    [RegularExpression(@"^([a-zA-Z0-9_-]{0,}#[0-9]{4})$")]
    public string ToNameTag { get; set; }
}