namespace Agree.Allow.Domain.Results;

using Agree.SharedKernel;

public class CreateAccountResult : Result<UserAccount, ErrorList>
{
    private CreateAccountResult(UserAccount user) : base((user))
    { }
    private CreateAccountResult(ErrorList error) : base(error)
    { }

    public static CreateAccountResult Ok(UserAccount user) => new(user);
    public static CreateAccountResult Fail(ErrorList data) => new(data);
}