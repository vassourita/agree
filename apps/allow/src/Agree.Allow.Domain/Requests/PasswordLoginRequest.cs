namespace Agree.Allow.Domain.Requests;

public record PasswordLoginRequest(string EmailAddress, string Password) : IAuthenticationRequest
{
}