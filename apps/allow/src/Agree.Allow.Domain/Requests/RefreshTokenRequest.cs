namespace Agree.Allow.Domain.Requests;

public record RefreshTokenRequest(string RefreshToken) : IAuthenticationRequest
{
}