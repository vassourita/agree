namespace Agree.Allow.Domain;

using System;
using Agree.SharedKernel;
using System.Security.Claims;

public class UserAccount : IEntity<Guid>
{
    public UserAccount(string username, string emailAddress, string passwordHash, DiscriminatorTag tag)
    {
        Id = Guid.NewGuid();
        Username = username;
        EmailAddress = emailAddress;
        PasswordHash = passwordHash;
        Tag = tag;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public string EmailAddress { get; private set; }

    public string PasswordHash { get; private set; }

    public string Username { get; private set; }

    public virtual string NameTag => $"{Username}#{Tag}";

    public DiscriminatorTag Tag { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
        new Claim(ClaimTypes.Name, NameTag),
        new Claim(ClaimTypes.Email, EmailAddress),
    }));

    public static UserAccount FromClaims(ClaimsPrincipal principal)
    {
        var id = Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
        var nameTag = principal.FindFirstValue(ClaimTypes.Name);
        var emailAddress = principal.FindFirstValue(ClaimTypes.Email);

        var username = nameTag.Split('#')[0];
        var tag = (DiscriminatorTag)Enum.Parse(typeof(DiscriminatorTag), nameTag.Split('#')[1]);

        return new UserAccount(username, emailAddress, null, tag) { Id = id };
    }
}