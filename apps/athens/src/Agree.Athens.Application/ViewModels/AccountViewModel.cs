using System;
using Agree.Athens.Domain.Aggregates.Account;

namespace Agree.Athens.Application.ViewModels
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Tag { get; set; }

        public string AvatarUrl { get; set; }

        public string UserNameWithTag { get => $"{UserName}#{Tag}"; }
    }
}