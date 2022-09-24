namespace Agree.Accord.Presentation.Responses;

using Agree.Accord.Presentation.Identity.ViewModels;

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