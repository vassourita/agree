using System;
using Agree.Athens.Domain.Aggregates.Account;

namespace Agree.Athens.Application.ViewModels
{
    public class LoggedUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public UserTag Tag { get; set; }
        public string Email { get; set; }
    }
}