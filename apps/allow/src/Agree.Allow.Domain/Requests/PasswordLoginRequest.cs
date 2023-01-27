namespace Agree.Allow.Domain.Requests;

using Agree.Allow.Domain.Results;
using MediatR;

/// <summary>
/// The login request.
/// </summary>
public record PasswordLoginRequest(string EmailAddress, string Password) : IAuthenticationRequest
{
}