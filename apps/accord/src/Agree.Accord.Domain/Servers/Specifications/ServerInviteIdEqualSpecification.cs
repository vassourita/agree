namespace Agree.Accord.Domain.Servers.Specifications;

using System;
using Agree.Accord.SharedKernel.Data;

public class ServerInviteIdEqualSpecification : Specification<ServerInvite>
{
    public ServerInviteIdEqualSpecification(Guid id)
        => Expression = x
        => x.Id == id;

    public ServerInviteIdEqualSpecification(ServerInvite invite)
        => Expression = x
        => x.Id == invite.Id;
}