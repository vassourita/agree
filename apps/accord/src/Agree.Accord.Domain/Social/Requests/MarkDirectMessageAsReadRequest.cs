namespace Agree.Accord.Domain.Social.Requests;

using System;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social.Results;
using MediatR;

public class MarkDirectMessageAsReadRequest : IRequest<DirectMessageResult>
{
    [Required]
    public Guid DirectMessageId { get; set; }

    [Required]
    public UserAccount Requester { get; set; }
}