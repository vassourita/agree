using System.Net.Mime;
using System;
using Agree.Accord.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace Agree.Accord.Domain.Social.Dtos
{
    public class SendFriendshipRequestDto
    {
        [Required]
        public ApplicationUser From { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_.-]{0,}#[0-9]{4})$")]
        public string ToNameTag { get; set; }
    }
}