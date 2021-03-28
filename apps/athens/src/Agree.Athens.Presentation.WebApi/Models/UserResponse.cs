using System;
using Agree.Athens.Application.ViewModels;
using Agree.Athens.Domain.Aggregates.Account;

namespace Agree.Athens.Presentation.WebApi.Models
{
    public class UserResponse : Response
    {
        public AccountViewModel User { get; private set; }

        public UserResponse(AccountViewModel user, string message = null)
            : base(message is null ? "You are currently logged in as {user.UserName}#{user.Tag}" : message)
        {
            User = new AccountViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Tag = user.Tag.ToString(),
                Email = user.Email,
                AvatarUrl = user.AvatarUrl
            };
        }

        public UserResponse(UserAccount user, string message = null)
            : base(message is null ? $"You are currently logged in as {user.UserName}#{user.Tag}" : message)
        {
            User = new AccountViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Tag = user.Tag.ToString(),
                Email = user.Email,
                AvatarUrl = user.AvatarUrl
            };
        }
    }
}