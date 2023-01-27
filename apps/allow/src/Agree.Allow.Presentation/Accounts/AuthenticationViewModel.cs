namespace Agree.Allow.Presentation.Accounts;

using Agree.Allow.Domain.Tokens;

public record AuthenticationViewModel(TokenCollection Tokens)
    : TokenCollection(Tokens.RefreshToken, Tokens.AccessToken);