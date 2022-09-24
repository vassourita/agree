namespace Agree.Accord.Domain.Social.Dtos;

using System;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;

public class SendDirectMessageDto
{
    [Required]
    public UserAccount From { get; set; }

    [Required]
    public Guid ToId { get; set; }

    [Required]
    [MaxLength(400)]
    public string MessageText { get; set; }

}