using System.Net.Mime;
using System;
using Agree.Accord.Domain.Identity;

namespace Agree.Accord.Domain.Social.Dtos
{
    public class FriendshipRequestDto
    {
        public ApplicationUser From { get; set; }
        public string ToNameTag { get; set; }
    }
}