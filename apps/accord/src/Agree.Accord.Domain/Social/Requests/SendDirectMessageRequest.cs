namespace Agree.Accord.Domain.Social.Requests;

using System;
using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Social.Results;
using MediatR;

/// <summary>
/// The request to send a direct message.
/// </summary>
public class SendDirectMessageRequest : IRequest<DirectMessageResult>
{
    public SendDirectMessageRequest() { }

    /// <summary>
    /// The user sending the direct message.
    /// </summary>
    /// <value>The sender user account.</value>
    [Required]
    public UserAccount From { get; set; }

    /// <summary>
    /// The user receiving the direct message.
    /// </summary>
    /// <value>The receiver user account id.</value>
    [Required]
    public Guid ToId { get; set; }

    /// <summary>
    /// The content of the direct message.
    /// </summary>
    /// <value>The message content.</value>
    [Required]
    [MaxLength(400)]
    public string MessageText { get; set; }

    /// <summary>
    /// The Id of the message this message is replying to.
    /// </summary>
    /// <value>The reply to message id.</value>
    public Guid? InReplyToId { get; set; }
}