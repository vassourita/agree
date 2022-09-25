namespace Agree.Accord.Domain.Social.Requests;

using System;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social.Results;
using MediatR;

public class SendDirectMessageRequest : IRequest<DirectMessageResult>
{
    [Required]
    public UserAccount From { get; set; }

    [Required]
    public Guid ToId { get; set; }

    [Required]
    [MaxLength(400)]
    public string MessageText { get; set; }

}