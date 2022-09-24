namespace Agree.Accord.Presentation.Responses;

using Agree.Accord.Presentation.Identity.ViewModels;

/// <summary>
/// The response to a user account request.
/// </summary>
public class UserResponse
{
    public UserResponse(ApplicationUserViewModel user) => User = user;

    public ApplicationUserViewModel User { get; private set; }
}