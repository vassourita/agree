namespace Agree.Allow.Domain.Requests;

using MediatR;

public record GetClientApplicationByAccessKeyRequest(string AccessKey) : IRequest<ClientApplication>
{
}