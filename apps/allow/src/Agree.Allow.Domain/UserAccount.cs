namespace Agree.Allow.Domain;

using System;
using Agree.SharedKernel;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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
        new Claim(JwtRegisteredClaimNames.Sub, Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Name, NameTag),
        new Claim(JwtRegisteredClaimNames.Email, EmailAddress)
    }));

    public static UserAccount FromClaims(ClaimsPrincipal principal)
    {
        var id = Guid.Parse(principal.FindFirstValue(JwtRegisteredClaimNames.Sub));
        var nameTag = principal.FindFirstValue(JwtRegisteredClaimNames.Name);
        var emailAddress = principal.FindFirstValue(JwtRegisteredClaimNames.Email);

        var username = nameTag.Split('#')[0];
        var tag = (DiscriminatorTag)Enum.Parse(typeof(DiscriminatorTag), nameTag.Split('#')[1]);

        return new UserAccount(username, emailAddress, null, tag) { Id = id };
    }
}