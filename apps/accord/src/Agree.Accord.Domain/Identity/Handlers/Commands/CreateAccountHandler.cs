namespace Agree.Accord.Domain.Identity.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Requests;
using Agree.Accord.Domain.Identity.Results;
using Agree.Accord.Domain.Identity.Tokens;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the creation of a new <see cref="UserAccount"/>.
/// </summary>
public class CreateAccountHandler : IRequestHandler<CreateAccountRequest, CreateAccountResult>
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly TokenFactory _tokenFactory;
    private readonly DiscriminatorTagFactory _discriminatorTagFactory;

    public CreateAccountHandler(IRepository<UserAccount, Guid> accountRepository, TokenFactory tokenFactory, IPasswordManager passwordManager, DiscriminatorTagFactory discriminatorTagFactory)
    {
        _accountRepository = accountRepository;
        _passwordManager = passwordManager;
        _tokenFactory = tokenFactory;
        _discriminatorTagFactory = discriminatorTagFactory;
    }

    public async Task<CreateAccountResult> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var validationResult = AnnotationValidator.TryValidate(request);

        if (validationResult.Failed)
            return CreateAccountResult.Fail(validationResult.Error.ToErrorList());

        var user = new UserAccount(
            request.Username,
            request.EmailAddress,
            await _passwordManager.HashAsync(request.Password),
            await _discriminatorTagFactory.GenerateDiscriminatorTagAsync(request.Username));

        await _accountRepository.InsertAsync(user);
        await _accountRepository.CommitAsync();

        var accessToken = await _tokenFactory.GenerateAccessTokenAsync(user);

        return CreateAccountResult.Ok(user, accessToken);
    }
}