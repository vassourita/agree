namespace Agree.Allow.Domain.Requests;

using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

public record GetAccountByIdRequest([Required] Guid Id) : IRequest<UserAccount>
{
}