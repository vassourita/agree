namespace Agree.Allow.Domain.Requests;

using Agree.Allow.Domain.Results;
using MediatR;

public interface IAuthenticationRequest : IRequest<AuthenticationResult>
{
}