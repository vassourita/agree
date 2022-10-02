namespace Agree.Accord.Domain.Servers.Handlers.Commands;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Specifications;
using Agree.Accord.Domain.Servers.Requests;
using Agree.Accord.Domain.Servers.Results;
using Agree.Accord.Domain.Servers.Specifications;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;

public class JoinServerHandler : IRequestHandler<JoinServerRequest, JoinServerResult>
{
    private readonly IRepository<Server, Guid> _serverRepository;
    private readonly IRepository<ServerInvite, Guid> _inviteRepository;
    private readonly IMediator _mediator;

    public JoinServerHandler(IRepository<Server, Guid> serverRepository, IRepository<ServerInvite, Guid> inviteRepository, IMediator mediator)
    {
        _serverRepository = serverRepository;
        _inviteRepository = inviteRepository;
        _mediator = mediator;
    }

    public async Task<JoinServerResult> Handle(JoinServerRequest request, CancellationToken cancellationToken)
    {
        var server = await _serverRepository.GetFirstAsync(new ServerIdEqualSpecification(request.ServerId));

        if (server is null)
            return JoinServerResult.Fail(new ErrorList("ServerId", "Server not found."));

        if (server.Members.Any(m => m.UserId == request.User.Id))
            return JoinServerResult.Fail(new ErrorList("User", "User is already a member of this server."));

        var member = new ServerMember(request.User, server);

        if (server.PrivacyLevel == ServerPrivacy.Public)
        {
            server.Members.Add(member);

            await _serverRepository.UpdateAsync(server);
            await _serverRepository.CommitAsync();

            return JoinServerResult.Ok(member);
        }

        if (request.InviteId is null)
            return JoinServerResult.Fail(new ErrorList("InviteId", "InviteId is required for private servers."));

        var invite = await _inviteRepository.GetFirstAsync(new ServerInviteIdEqualSpecification(request.InviteId.Value));

        var errorList = new ErrorList();

        if (invite.Server.Id != request.ServerId)
            errorList.AddError("InviteId", "Invite is not valid for this server.");

        if (invite.ExpirationDate < DateTime.UtcNow)
            errorList.AddError("InviteId", "Invite has expired.");

        if (errorList.HasErrors("InviteId"))
            return JoinServerResult.Fail(errorList);

        server.Members.Add(member);

        await _serverRepository.UpdateAsync(server);
        await _serverRepository.CommitAsync();

        return JoinServerResult.Ok(member);
    }
}