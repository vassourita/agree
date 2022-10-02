namespace Agree.Accord.Presentation.Servers.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using Agree.Accord.Domain.Servers;
using Agree.Accord.Presentation.Identity.ViewModels;

/// <summary>
/// A role view model.
/// </summary>
public class RoleViewModel
{
    /// <summary>
    /// The role Id.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// The role name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The server this role belongs to.
    /// </summary>
    public ServerViewModel Server { get; private set; }

    /// <summary>
    /// The server members with this role.
    /// </summary>
    public IEnumerable<UserAccountViewModel> Members { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="RoleViewModel"/> class from a server entity.
    /// </summary>
    /// <param name="entity">The server.</param>
    /// <returns>The view model.</returns>
    public static RoleViewModel FromEntity(ServerRole entity, bool hideServer = false) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Server = hideServer ? null : ServerViewModel.FromEntity(entity.Server),
        Members = entity.ServerMembers.Select(sm => UserAccountViewModel.FromEntity(sm.User))
    };
}