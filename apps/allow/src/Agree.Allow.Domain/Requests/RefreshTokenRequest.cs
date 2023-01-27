namespace Agree.Allow.Domain.Requests;

using Agree.Allow.Domain.Results;
using MediatR;

/// <summary>
/// The refresh request.
/// </summary>
public record RefreshTokenRequest(string RefreshToken) : IAuthenticationRequest
{
}