namespace Agree.Accord.Domain.Social.Requests;

using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

public class GetDirectMessagebyIdRequest : IRequest<DirectMessage>
{
    [Required]
    public Guid Id { get; set; }
}