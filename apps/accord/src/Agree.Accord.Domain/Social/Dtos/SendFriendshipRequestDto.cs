namespace Agree.Accord.Domain.Social.Dtos;

using Agree.Accord.Domain.Identity;
using System.ComponentModel.DataAnnotations;

public class SendFriendshipRequestDto
{
    [Required]
    public UserAccount From { get; set; }

    [Required]
    [RegularExpression(@"^([a-zA-Z0-9_-]{0,}#[0-9]{4})$")]
    public string ToNameTag { get; set; }
}