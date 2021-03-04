using System;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.SharedKernel;

namespace Agree.Athens.Application.Dtos
{
    public class UpdateAccountDto : Validatable
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public UserTag Tag { get; set; }

        public string PasswordConfirmation { get; set; }
    }
}