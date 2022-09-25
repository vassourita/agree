namespace Agree.Accord.Domain.Social.Requests;

using System;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social.Results;
using MediatR;

/// <summary>
/// The request to mark a direct message as read.
/// </summary>
public class MarkDirectMessageAsReadRequest : IRequest<DirectMessageResult>
{
    public MarkDirectMessageAsReadRequest() { }

    /// <summary>
    /// The direct message Id to be marked as read.
    /// </summary>
    /// <value>The direct message Id.</value>
    [Required]
    public Guid DirectMessageId { get; set; }

    /// <summary>
    /// The user marking the direct message as read.
    /// </summary>
    /// <value>The user account.</value>
    [Required]
    public UserAccount Requester { get; set; }
}