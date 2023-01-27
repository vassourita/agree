namespace Agree.Allow.Domain.Results;

using Agree.Allow.Domain.Tokens;
using Agree.SharedKernel;

/// <summary>
/// The result of a login operation.
/// </summary>
public class TokenValidationResult : Result
{
    private TokenValidationResult(bool succeeded) : base(succeeded)
    { }

    public static TokenValidationResult Ok() => new(true);
    public static TokenValidationResult Fail() => new(false);
}