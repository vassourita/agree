namespace Agree.Accord.Domain.Identity.Requests;

using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

public class GetAccountByIdRequest : IRequest<UserAccount>
{
    public GetAccountByIdRequest(Guid id) => Id = id;
    public GetAccountByIdRequest() { }

    [Required]
    public Guid Id { get; set; }
}