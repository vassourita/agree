namespace Agree.Accord.Domain.Servers.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Servers.Requests;
using Agree.Accord.Domain.Servers.Results;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;

public class CreateServerHandler : IRequestHandler<CreateServerRequest, CreateServerResult>
{
    private readonly IRepository<Server, Guid> _serverRepository;

    public CreateServerHandler(IRepository<Server, Guid> serverRepository)
        => _serverRepository = serverRepository;

    public async Task<CreateServerResult> Handle(CreateServerRequest request, CancellationToken cancellationToken)
    {
        var validationResult = AnnotationValidator.TryValidate(request);

        if (validationResult.Failed)
            return CreateServerResult.Fail(validationResult.Error.ToErrorList());

        var server = new Server(request.Name, request.PrivacyLevel, request.Description);

        var category = Category.CreateDefaultWelcomeCategory(server);
        server.Categories.Add(category);

        var adminRole = ServerRole.CreateDefaultAdminRole(server);
        server.Roles.Add(adminRole);

        var ownerAsMember = new ServerMember(request.Owner, server);
        ownerAsMember.Roles.Add(adminRole);
        server.Members.Add(ownerAsMember);

        await _serverRepository.InsertAsync(server);
        await _serverRepository.CommitAsync();

        return CreateServerResult.Ok(server);
    }
}