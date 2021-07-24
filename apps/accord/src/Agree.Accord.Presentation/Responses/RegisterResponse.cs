using Agree.Accord.Presentation.Identity.ViewModels;

namespace Agree.Accord.Presentation.Responses
{
    public class RegisterResponse : UserResponse
    {
        public RegisterResponse(ApplicationUserViewModel user) : base(user)
        {
        }
        public string Message => $"Account created succesfully.";
    }
}