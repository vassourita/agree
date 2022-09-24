namespace Agree.Accord.Domain.Identity.Results;

using Agree.Accord.Domain.Identity.Tokens;
using Agree.Accord.SharedKernel;

/// <summary>
/// The result of a login operation.
/// </summary>
public class LoginResult : Result<AccessToken>
{
    private LoginResult(AccessToken data) : base(data)
    { }
    private LoginResult() : base()
    { }

    public static LoginResult Ok(AccessToken data) => new(data);
    public static LoginResult Fail() => new();
}