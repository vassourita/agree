namespace Agree.Allow.Domain.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Allow.Domain.Requests;
using Agree.Allow.Domain.Results;
using Agree.Allow.Domain.Specifications;
using Agree.Allow.Domain.Tokens;
using Agree.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles login requests using a password.
/// </summary>
public class PasswordLoginHandler : IRequestHandler<PasswordLoginRequest, AuthenticationResult>
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly AccessTokenFactory _accessTokenFactory;
    private readonly RefreshTokenFactory _refreshTokenFactory;

    public PasswordLoginHandler(IRepository<UserAccount, Guid> accountRepository,
                        AccessTokenFactory acessTokenFactory,
                        RefreshTokenFactory refreshTokenFactory,
                        IPasswordManager passwordManager)
    {
        _accountRepository = accountRepository;
        _accessTokenFactory = acessTokenFactory;
        _refreshTokenFactory = refreshTokenFactory;
        _passwordManager = passwordManager;
    }

    public async Task<AuthenticationResult> Handle(PasswordLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.GetFirstAsync(new EmailEqualSpecification(request.EmailAddress));

        if (user == null)
            return AuthenticationResult.Fail();

        var passwordValid = await _passwordManager.CompareAsync(user.PasswordHash, request.Password);

        if (!passwordValid)
            return AuthenticationResult.Fail();

        var accessToken = await _accessTokenFactory.GenerateAsync(user);
        var refreshToken = await _refreshTokenFactory.GenerateAsync(user);

        return AuthenticationResult.Ok(new TokenCollection(accessToken, refreshToken));
    }
}