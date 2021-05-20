using Agree.Allow.Presentation.ViewModels;

namespace Agree.Allow.Presentation.Responses
{
    public class UserResponse
    {
        public UserResponse(ApplicationUserViewModel user)
        {
            User = user;
            Message = $"You are currently logged-in as {user.UserName}#{user.Tag}";
        }
        public string Message { get; set; }
        public ApplicationUserViewModel User { get; set; }
    }
}