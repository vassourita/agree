using Agree.Accord.Presentation.ViewModels;

namespace Agree.Accord.Presentation.Responses
{
    public class UserResponse
    {
        public UserResponse(ApplicationUserViewModel user)
        {
            User = user;
        }

        public ApplicationUserViewModel User { get; private set; }
    }
}