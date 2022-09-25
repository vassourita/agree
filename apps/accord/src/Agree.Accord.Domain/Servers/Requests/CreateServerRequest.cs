namespace Agree.Accord.Domain.Servers.Requests;

using System.ComponentModel.DataAnnotations;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Servers.Results;
using MediatR;

/// <summary>
/// The representation of a request to create a new server.
/// </summary>
public class CreateServerRequest : IRequest<CreateServerResult>
{
    /// <summary>
    /// The server name.
    /// </summary>
    /// <value>The server name.</value>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    /// <summary>
    /// The server description.
    /// </summary>
    /// <value>The server description.</value>
    [MaxLength(300)]
    public string Description { get; set; }

    /// <summary>
    /// The server privacy level.
    /// </summary>
    /// <value>The server privacy level.</value>
    [Required]
    public ServerPrivacy PrivacyLevel { get; set; }

    /// <summary>
    /// The user that created the server.
    /// </summary>
    /// <value>The user that created the server.</value>
    public UserAccount Owner { get; set; }
}