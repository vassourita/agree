namespace Agree.Accord.Domain.Servers.Requests;

using System;
using System.Collections.Generic;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Represents a request to get a server by its id, taking into account the server's privacy settings.
/// </summary>
public class GetServerByIdRequest : IRequest<Server>
{
    /// <summary>
    /// The id of the server to get.
    /// </summary>
    /// <value>The id of the server.</value>
    public Guid ServerId { get; set; }

    /// <summary>
    /// The id of the user that is searching.
    /// Useful to filter out secret servers.
    /// </summary>
    /// <value>The id of the user that is searching.</value>
    public Guid UserId { get; set; }
}