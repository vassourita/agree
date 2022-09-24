namespace Agree.Accord.Presentation.Responses;

using Agree.Accord.Presentation.Identity.ViewModels;

/// <summary>
/// The response to a user account request.
/// </summary>
public class UserResponse
{
    public UserResponse(UserAccountViewModel user) => User = user;

    public UserAccountViewModel User { get; private set; }
}