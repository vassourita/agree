namespace Agree.Allow.Domain.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Allow.Domain.Requests;
using Agree.Allow.Domain.Results;
using Agree.Allow.Domain.Tokens;
using Agree.SharedKernel.Data;
using MediatR;

public class ValidateTokenHandler : IRequestHandler<ValidateTokenRequest, TokenValidationResult>
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;
    private readonly AccessTokenFactory _accessTokenFactory;
    private readonly RefreshTokenFactory _refreshTokenFactory;
    private readonly TokenValidator _tokenValidator;

    public ValidateTokenHandler(IRepository<UserAccount, Guid> accountRepository,
                        AccessTokenFactory accessTokenFactory,
                        RefreshTokenFactory refreshTokenFactory,
                        TokenValidator tokenValidator)
    {
        _accountRepository = accountRepository;
        _accessTokenFactory = accessTokenFactory;
        _refreshTokenFactory = refreshTokenFactory;
        _tokenValidator = tokenValidator;
    }

    public async Task<TokenValidationResult> Handle(ValidateTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _tokenValidator.ValidateAsync(request.Token);

        if (user == null)
            return TokenValidationResult.Fail();

        return TokenValidationResult.Ok(user);
    }
}