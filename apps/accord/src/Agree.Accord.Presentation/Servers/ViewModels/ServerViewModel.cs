namespace Agree.Accord.Presentation.Servers.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using Agree.Accord.Domain.Servers;
using Agree.Accord.Presentation.Identity.ViewModels;

/// <summary>
/// A server view model.
/// </summary>
public class ServerViewModel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string PrivacyLevel { get; private set; }

    public IEnumerable<RoleViewModel> Roles { get; private set; }

    public IEnumerable<UserAccountViewModel> Members { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ServerViewModel"/> class from a server entity.
    /// </summary>
    /// <param name="entity">The server.</param>
    /// <returns>The view model.</returns>
    public static ServerViewModel FromEntity(Server entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description,
        PrivacyLevel = entity.PrivacyLevel.ToString(),
        Roles = entity.Roles.Select(r => RoleViewModel.FromEntity(r, true)),
        Members = entity.Members.Select(sm => UserAccountViewModel.FromEntity(sm.User))
    };
}