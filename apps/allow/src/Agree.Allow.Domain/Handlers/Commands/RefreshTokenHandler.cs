namespace Agree.Allow.Domain.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Allow.Domain.Requests;
using Agree.Allow.Domain.Results;
using Agree.Allow.Domain.Tokens;
using Agree.SharedKernel.Data;
using MediatR;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, AuthenticationResult>
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;
    private readonly AccessTokenFactory _accessTokenFactory;
    private readonly RefreshTokenFactory _refreshTokenFactory;
    private readonly TokenValidator _tokenValidator;

    public RefreshTokenHandler(IRepository<UserAccount, Guid> accountRepository,
                        AccessTokenFactory accessTokenFactory,
                        RefreshTokenFactory refreshTokenFactory,
                        TokenValidator tokenValidator)
    {
        _accountRepository = accountRepository;
        _accessTokenFactory = accessTokenFactory;
        _refreshTokenFactory = refreshTokenFactory;
        _tokenValidator = tokenValidator;
    }

    public async Task<AuthenticationResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _tokenValidator.ValidateAsync(request.RefreshToken, true);

        if (user == null)
            return AuthenticationResult.Fail();

        var accessToken = await _accessTokenFactory.GenerateAsync(user);
        var refreshToken = await _refreshTokenFactory.GenerateAsync(user);

        return AuthenticationResult.Ok(new TokenCollection(accessToken, refreshToken));
    }
}