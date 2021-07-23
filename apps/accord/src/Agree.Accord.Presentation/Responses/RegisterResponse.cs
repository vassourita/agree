using Agree.Accord.Presentation.ViewModels;

namespace Agree.Accord.Presentation.Responses
{
    public class RegisterResponse
    {
        public RegisterResponse(ApplicationUserViewModel user)
        {
            User = user;
        }

        public ApplicationUserViewModel User { get; private set; }
    }
}