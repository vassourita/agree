namespace Agree.Accord.Presentation.Identity.ViewModels;

using System.Security.Claims;
using System;
using Agree.Accord.Domain.Identity;
using System.Linq;

/// <summary>
/// A view model for a user account.
/// </summary>
public class ApplicationUserViewModel
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string NameTag => $"{Username}#{Tag}";
    public string Tag { get; private set; }
    public bool Verified { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ApplicationUserViewModel"/> class from a <see cref="ApplicationUser"/> entity.
    /// </summary>
    /// <param name="entity">The user account.</param>
    /// <returns>The view model.</returns>
    public static ApplicationUserViewModel FromEntity(ApplicationUser entity) => new()
    {
        Id = entity.Id,
        Username = entity.Username,
        Tag = entity.Tag.ToString(),
        Verified = entity.EmailConfirmed
    };

    /// <summary>
    /// Creates a new instance of the <see cref="ApplicationUserViewModel"/> class from a <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>The view model.</returns>
    public static ApplicationUserViewModel FromClaims(ClaimsPrincipal principal)
    {
        var nameTag = principal.Identity.Name;
        var split = nameTag.Split('#');
        return new ApplicationUserViewModel
        {
            Id = Guid.Parse(principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value),
            Username = split.First(),
            Tag = split.Last(),
            Verified = bool.Parse(principal.Claims.First(c => c.Type == "verified").Value)
        };
    }
}