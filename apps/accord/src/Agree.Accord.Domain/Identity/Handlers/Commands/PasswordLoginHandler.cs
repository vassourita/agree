namespace Agree.Accord.Domain.Identity.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Requests;
using Agree.Accord.Domain.Identity.Results;
using Agree.Accord.Domain.Identity.Specifications;
using Agree.Accord.Domain.Identity.Tokens;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles login requests using a password.
/// </summary>
public class PasswordLoginHandler : IRequestHandler<PasswordLoginRequest, PasswordLoginResult>
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly TokenFactory _tokenFactory;

    public PasswordLoginHandler(IRepository<UserAccount, Guid> accountRepository, TokenFactory tokenFactory, IPasswordManager passwordManager)
    {
        _accountRepository = accountRepository;
        _tokenFactory = tokenFactory;
        _passwordManager = passwordManager;
    }

    public async Task<PasswordLoginResult> Handle(PasswordLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.GetFirstAsync(new EmailEqualSpecification(request.EmailAddress));

        if (user == null)
            return PasswordLoginResult.Fail();

        var passwordValid = await _passwordManager.CompareAsync(user.PasswordHash, request.Password);

        if (!passwordValid)
            return PasswordLoginResult.Fail();

        var accessToken = await _tokenFactory.GenerateAccessTokenAsync(user);

        return PasswordLoginResult.Ok(accessToken);
    }
}