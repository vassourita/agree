namespace Agree.Accord.Domain.Identity.Commands;

using Agree.Accord.Domain.Identity.Results;
using MediatR;

/// <summary>
/// The login request.
/// </summary>
public class LoginCommand : IRequest<LoginResult>
{
    /// <summary>
    /// The user email address for logging in.
    /// </summary>
    /// <value>The email address.</value>
    public string Email { get; set; }

    /// <summary>
    /// The user password for logging in.
    /// </summary>
    /// <value>The password.</value>
    public string Password { get; set; }
}