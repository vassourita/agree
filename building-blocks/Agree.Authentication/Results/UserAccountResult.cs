namespace Agree.Authentication.Results;

using System.Security.Claims;

public class UserAccountResult
{
    public Guid Id { get; set; }
    public string Tag { get; set; }
    public string NameTag { get; set; }
    public string Username { get; set; }

    public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
        new Claim(ClaimTypes.Name, NameTag)
    }));
}