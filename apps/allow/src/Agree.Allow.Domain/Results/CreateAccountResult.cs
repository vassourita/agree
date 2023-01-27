namespace Agree.Allow.Domain.Results;

using Agree.Allow.Domain.Tokens;
using Agree.SharedKernel;

/// <summary>
/// The result of a attempt to create a new account.
/// </summary>
public class CreateAccountResult : Result<UserAccount, ErrorList>
{
    private CreateAccountResult(UserAccount user) : base((user))
    { }
    private CreateAccountResult(ErrorList error) : base(error)
    { }

    public static CreateAccountResult Ok(UserAccount user) => new(user);
    public static CreateAccountResult Fail(ErrorList data) => new(data);
}