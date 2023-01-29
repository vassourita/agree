namespace Agree.Authentication;

using Agree.Authentication.Requests;
using Agree.Authentication.Results;

public interface IAuthenticationProvider
{
    Task<AuthenticationResult> AuthenticateAsync(AuthenticationRequest request);
    Task<AuthenticationResult> RefreshTokenAsync();
    Task<UserAccountResult> GetUserAccountAsync();
}
