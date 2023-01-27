namespace Agree.Allow.Presentation.Accounts.ViewModels;

using Agree.Allow.Domain.Tokens;

public record AuthenticationViewModel(TokenCollection Tokens)
    : TokenCollection(Tokens.RefreshToken, Tokens.AccessToken);