namespace Agree.Accord.Domain.Servers.Requests;

using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using MediatR;
using Agree.Accord.Domain.Servers.Results;
using System;

/// <summary>
/// The representation of a request to create a new category.
/// </summary>
public class CreateCategoryRequest : IRequest<CreateCategoryResult>
{
    /// <summary>
    /// The server id that the category belongs to.
    /// </summary>
    /// <value>The server id.</value>
    [Required]
    public Guid ServerId { get; set; }

    /// <summary>
    /// The category name.
    /// </summary>
    /// <value>The category name.</value>
    [Required]
    [MaxLength(80)]
    public string Name { get; set; }

    /// <summary>
    /// The category position when displayed.
    /// </summary>
    /// <value>The category position when displayed.</value>
    [Required]
    public int Position { get; set; }

    /// <summary>
    /// The user that created the category.
    /// </summary>
    /// <value>The user that created the category.</value>
    public UserAccount Requester { get; set; }
}