namespace Agree.Accord.Domain.Social.Dtos;

using System;
using Agree.Accord.Domain.Identity;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The request to accept a friendship request.
/// </summary>
public class AcceptFriendshipRequestDto
{
    /// <summary>
    /// The user accepting the friendship request.
    /// </summary>
    /// <value>The user account.</value>
    [Required]
    public ApplicationUser LoggedUser { get; set; }

    /// <summary>
    /// The id of the friendship request sender.
    /// </summary>
    /// <value>The sender account id.</value>
    [Required]
    public Guid FromUserId { get; set; }
}