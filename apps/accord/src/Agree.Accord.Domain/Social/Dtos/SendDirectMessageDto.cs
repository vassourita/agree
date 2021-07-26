using System;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;

namespace Agree.Accord.Domain.Social.Dtos
{
    public class SendDirectMessageDto
    {
        [Required]
        public ApplicationUser From { get; set; }

        [Required]
        public Guid ToId { get; set; }

        [Required]
        public string MessageText { get; set; }

    }
}