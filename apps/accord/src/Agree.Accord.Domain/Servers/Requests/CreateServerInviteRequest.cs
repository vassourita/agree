namespace Agree.Accord.Domain.Servers.Requests;

using System;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Servers.Results;
using MediatR;

public class CreateServerInviteRequest : IRequest
{
    public Guid ServerId { get; set; }
    public UserAccount Inviter { get; set; }
    public DateTime ExpirationDate { get; set; }
}