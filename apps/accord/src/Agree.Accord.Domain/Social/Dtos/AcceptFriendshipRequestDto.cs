using System.Net.Mime;
using System;
using Agree.Accord.Domain.Identity;

namespace Agree.Accord.Domain.Social.Dtos
{
    public class AcceptFriendshipRequestDto
    {
        public ApplicationUser LoggedUser { get; set; }
        public Guid FromUserId { get; set; }
    }
}