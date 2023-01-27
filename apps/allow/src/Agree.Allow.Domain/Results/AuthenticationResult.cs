namespace Agree.Allow.Domain.Results;

using Agree.Allow.Domain.Tokens;
using Agree.SharedKernel;

/// <summary>
/// The result of a login or refresh operation.
/// </summary>
public class AuthenticationResult : Result<TokenCollection>
{
    private AuthenticationResult(TokenCollection tokens) : base(tokens)
    { }
    private AuthenticationResult() : base()
    { }

    public static AuthenticationResult Ok(TokenCollection tokens) => new(tokens);
    public static AuthenticationResult Fail() => new();
}