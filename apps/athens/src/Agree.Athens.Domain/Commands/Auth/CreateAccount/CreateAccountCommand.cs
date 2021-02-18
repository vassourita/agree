using System;
using MediatR;

namespace Agree.Athens.Domain.Commands.Auth.CreateAccount
{
    public class CreateAccountCommand : IRequest<Guid>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}