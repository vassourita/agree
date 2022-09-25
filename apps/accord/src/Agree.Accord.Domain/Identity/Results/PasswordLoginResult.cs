namespace Agree.Accord.Domain.Identity.Results;

using Agree.Accord.Domain.Identity.Tokens;
using Agree.Accord.SharedKernel;

/// <summary>
/// The result of a login operation.
/// </summary>
public class PasswordLoginResult : Result<AccessToken>
{
    private PasswordLoginResult(AccessToken data) : base(data)
    { }
    private PasswordLoginResult() : base()
    { }

    public static PasswordLoginResult Ok(AccessToken data) => new(data);
    public static PasswordLoginResult Fail() => new();
}