using Agree.Allow.Presentation.ViewModels;

namespace Agree.Allow.Presentation.Responses
{
    public class UserResponse
    {
        public UserResponse(UserAccountViewModel user)
        {
            User = user;
            Message = $"You are currently logged-in as {user.UserName}#{user.Tag}";
        }
        public string Message { get; set; }
        public UserAccountViewModel User { get; set; }
    }
}