namespace Agree.Allow.Domain.Results;

using Agree.SharedKernel;

public class TokenValidationResult : Result<UserAccount>
{
    private TokenValidationResult(UserAccount user) : base(user)
    { }
    private TokenValidationResult() : base()
    { }

    public static TokenValidationResult Ok(UserAccount user) => new(user);
    public static TokenValidationResult Fail() => new();
}