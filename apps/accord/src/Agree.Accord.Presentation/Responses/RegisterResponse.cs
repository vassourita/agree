using Agree.Accord.Presentation.Identity.ViewModels;

namespace Agree.Accord.Presentation.Responses
{
    /// <summary>
    /// The response to a successful Register request.
    /// </summary>
    public class RegisterResponse : UserResponse
    {
        public RegisterResponse(ApplicationUserViewModel user) : base(user)
        {
        }
        public string Message => $"Account created succesfully.";
    }
}