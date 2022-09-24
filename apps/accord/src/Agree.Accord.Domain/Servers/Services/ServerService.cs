namespace Agree.Accord.Domain.Servers.Services;

using System;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Servers.Dtos;
using Agree.Accord.Domain.Servers.Results;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using Microsoft.AspNetCore.Identity;

/// <summary>
/// Provides methods for creating and managing servers.
/// </summary>
public class ServerService
{
    private readonly IRepository<Server> _serverRepository;
    private readonly IRepository<ServerRole> _serverRoleRepository;
    private readonly IRepository<IdentityUserRole<Guid>> _userRoleRepository;

    public ServerService(IRepository<Server> serverRepository,
                         IRepository<IdentityUserRole<Guid>> userRoleRepository,
                         IRepository<ServerRole> serverRoleRepository)
    {
        _serverRepository = serverRepository;
        _userRoleRepository = userRoleRepository;
        _serverRoleRepository = serverRoleRepository;
    }

    /// <summary>
    /// Creates a new server.
    /// </summary>
    /// <param name="createServerDto">The creation request data.</param>
    /// <param name="owner">The user creating the server.</param>
    /// <returns>The result of the creation.</returns>
    public async Task<CreateServerResult> CreateServerAsync(CreateServerDto createServerDto, ApplicationUser owner)
    {
        var validationResult = AnnotationValidator.TryValidate(createServerDto);

        if (validationResult.Failed)
        {
            return CreateServerResult.Fail(validationResult.Error.ToErrorList());
        }

        var server = new Server(createServerDto.Name, createServerDto.PrivacyLevel, createServerDto.Description);
        var category = Category.CreateDefaultWelcomeCategory(server);
        server.Categories.Add(category);
        server.Members.Add(owner);

        var adminRole = ServerRole.CreateDefaultAdminRole(server);

        await _serverRepository.InsertAsync(server);
        await _serverRoleRepository.InsertAsync(adminRole);
        await _userRoleRepository.InsertAsync(new IdentityUserRole<Guid>
        {
            UserId = owner.Id,
            RoleId = adminRole.Id
        });
        await _serverRepository.CommitAsync();

        return CreateServerResult.Ok(server);
    }
}