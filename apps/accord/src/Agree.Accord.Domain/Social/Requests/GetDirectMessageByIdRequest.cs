namespace Agree.Accord.Domain.Social.Requests;

using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

/// <summary>
/// The request to get a direct message by its Id.
/// </summary>
public class GetDirectMessagebyIdRequest : IRequest<DirectMessage>
{
    /// <summary>
    /// The id of the direct message to be retrieved.
    /// </summary>
    /// <value>The direct message id.</value>
    [Required]
    public Guid Id { get; set; }
}