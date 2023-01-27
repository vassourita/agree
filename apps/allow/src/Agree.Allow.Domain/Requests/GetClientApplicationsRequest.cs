namespace Agree.Allow.Domain.Requests;

using MediatR;

public record GetClientApplicationsRequest() : IRequest<IEnumerable<ClientApplication>>
{
}