namespace Agree.Allow.Domain.Requests;

using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

/// <summary>
/// Represents a request to get an account by its id.
/// </summary>
public record GetAccountByIdRequest([Required] Guid Id) : IRequest<UserAccount>
{
}