namespace Agree.Accord.Domain.Servers.Requests;

using System;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Servers.Results;
using MediatR;

public class JoinServerRequest : IRequest<JoinServerResult>
{
    public Guid ServerId { get; set; }
    public UserAccount User { get; set; }
    public Guid? InviteId { get; set; }
}