namespace Agree.Allow.Presentation.Accounts.ViewModels;

using System.Security.Claims;
using System;
using Agree.Allow.Domain;
using System.Linq;

/// <summary>
/// A view model for a user account.
/// </summary>
public class UserAccountViewModel
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Tag { get; private set; }
    public string NameTag => $"{Username}#{Tag}";

    /// <summary>
    /// Creates a new instance of the <see cref="UserAccountViewModel"/> class from a <see cref="UserAccount"/> entity.
    /// </summary>
    /// <param name="entity">The user account.</param>
    /// <returns>The view model.</returns>
    public static UserAccountViewModel FromEntity(UserAccount entity) => new()
    {
        Id = entity.Id,
        Username = entity.Username,
        Tag = entity.Tag.ToString(),
    };

    /// <summary>
    /// Creates a new instance of the <see cref="UserAccountViewModel"/> class from a <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>The view model.</returns>
    public static UserAccountViewModel FromClaims(ClaimsPrincipal principal)
    {
        var nameTag = principal.Identity.Name;
        var split = nameTag.Split('#');
        return new UserAccountViewModel
        {
            Id = Guid.Parse(principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value),
            Username = split.First(),
            Tag = split.Last(),
        };
    }
}