using Agree.Accord.Presentation.Identity.ViewModels;

namespace Agree.Accord.Presentation.Responses
{
    /// <summary>
    /// The response to a user account request.
    /// </summary>
    public class UserResponse
    {
        public UserResponse(ApplicationUserViewModel user)
        {
            User = user;
        }

        public ApplicationUserViewModel User { get; private set; }
    }
}