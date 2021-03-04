using System;
using Agree.Athens.Application.ViewModels;

namespace Agree.Athens.Presentation.WebApi.Models
{
    public class UserResponse : Response
    {
        public record LoggedUserInfo(Guid Id, string UserName, string Tag, string Email);

        public LoggedUserInfo User { get; private set; }

        public UserResponse(AccountViewModel user)
            : base(user == null
                   ? ("You are not logged in")
                   : ($"You are currently logged in as {user.UserName}#{user.Tag}"))
        {
            User = new LoggedUserInfo(user.Id, user.UserName, user.Tag.ToString(), user.Email);
        }
    }
}