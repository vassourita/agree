using System;
using System.Linq;
using Agree.Accord.Domain.Servers;
using Agree.Accord.SharedKernel.Data;

public class ServerVisibleToUserSpecification : Specification<Server>
{
    public ServerVisibleToUserSpecification(Guid userId)
        => Expression = x
        => (x.PrivacyLevel == ServerPrivacy.Public || x.PrivacyLevel == ServerPrivacy.Private)
            || x.Members.Any(m => m.UserId == userId);
}