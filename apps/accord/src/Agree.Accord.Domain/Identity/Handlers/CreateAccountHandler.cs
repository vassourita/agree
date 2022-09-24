namespace Agree.Accord.Domain.Identity.Handlers;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Commands;
using Agree.Accord.Domain.Identity.Results;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, CreateAccountResult>
{
    private readonly IUserAccountRepository _accountRepository;
    private readonly AccountService _accountService;
    private readonly IPasswordManager _passwordManager;
    private readonly TokenService _tokenService;

    public CreateAccountHandler(IUserAccountRepository accountRepository, AccountService accountService, TokenService tokenService, IPasswordManager passwordManager)
    {
        _accountRepository = accountRepository;
        _accountService = accountService;
        _tokenService = tokenService;
        _passwordManager = passwordManager;
    }

    public async Task<CreateAccountResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var validationResult = AnnotationValidator.TryValidate(request);

        if (validationResult.Failed)
        {
            return CreateAccountResult.Fail(validationResult.Error.ToErrorList());
        }

        var user = new UserAccount
        {
            Id = Guid.NewGuid(),
            EmailAddress = request.EmailAddress,
            PasswordHash = await _passwordManager.HashAsync(request.Password),
            Username = request.Username,
            Tag = await _accountService.GenerateDiscriminatorTagAsync(request.Username),
            CreatedAt = DateTime.UtcNow
        };

        user = await _accountRepository.InsertAsync(user);
        await _accountRepository.CommitAsync();

        var accessToken = await _tokenService.GenerateAccessTokenAsync(user);

        return CreateAccountResult.Ok(user, accessToken);
    }
}