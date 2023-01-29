namespace Agree.Allow.Presentation.Accounts.ViewModels;

using System.Security.Claims;
using System;
using Agree.Allow.Domain;
using System.Linq;

public class UserAccountViewModel
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Tag { get; private set; }
    public string NameTag => $"{Username}#{Tag}";

    public static UserAccountViewModel FromEntity(UserAccount entity) => new()
    {
        Id = entity.Id,
        Username = entity.Username,
        Tag = entity.Tag.ToString(),
    };

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