namespace Agree.Accord.Domain.Social.Requests;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

/// <summary>
/// The request to get the chat between two users.
/// </summary>
public class GetFriendChatRequest : IRequest<IEnumerable<DirectMessage>>
{
    public GetFriendChatRequest() { }

    /// <summary>
    /// The id of the direct message to be retrieved.
    /// </summary>
    /// <value>The friend id.</value>
    [Required]
    public Guid FriendId { get; set; }

    /// <summary>
    /// The id of the user who is retrieving the chat.
    /// </summary>
    /// <value>The user id.</value>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// The number of items to be retrieved.
    /// </summary>
    /// <value>The number of items.</value>
    [Required]
    [Range(1, 100)]
    public int PageSize { get; set; }

    /// <summary>
    /// The Id of the first item to be retrieved.
    /// Determines from which item the pagination will start.
    /// </summary>
    /// <value>The first item id.</value>
    public Guid StartAtId { get; set; }
}