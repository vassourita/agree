namespace Agree.Allow.Domain.Requests;

using Agree.Allow.Domain.Results;
using MediatR;

public record ValidateTokenRequest(string Token) : IRequest<TokenValidationResult>
{
}