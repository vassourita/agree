namespace Agree.Accord.Domain.Identity.Requests;

using Agree.Accord.Domain.Identity.Results;
using MediatR;

/// <summary>
/// The login request.
/// </summary>
public class PasswordLoginRequest : IRequest<PasswordLoginResult>
{
    /// <summary>
    /// The user email address for logging in.
    /// </summary>
    /// <value>The email address.</value>
    public string EmailAddress { get; set; }

    /// <summary>
    /// The user password for logging in.
    /// </summary>
    /// <value>The password.</value>
    public string Password { get; set; }
}