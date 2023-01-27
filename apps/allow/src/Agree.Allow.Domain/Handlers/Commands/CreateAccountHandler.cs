namespace Agree.Allow.Domain.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Allow.Domain.Requests;
using Agree.Allow.Domain.Results;
using Agree.Allow.Domain.Specifications;
using Agree.Allow.Domain.Tokens;
using Agree.SharedKernel;
using Agree.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the creation of a new <see cref="UserAccount"/>.
/// </summary>
public class CreateAccountHandler : IRequestHandler<CreateAccountRequest, CreateAccountResult>
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly AccessTokenFactory _accessTokenFactory;
    private readonly RefreshTokenFactory _refreshTokenFactory;
    private readonly DiscriminatorTagFactory _discriminatorTagFactory;

    public CreateAccountHandler(IRepository<UserAccount, Guid> accountRepository,
                            AccessTokenFactory accessTokenFactory,
                            RefreshTokenFactory refreshTokenFactory,
                            IPasswordManager passwordManager,
                            DiscriminatorTagFactory discriminatorTagFactory)
    {
        _accountRepository = accountRepository;
        _passwordManager = passwordManager;
        _accessTokenFactory = accessTokenFactory;
        _refreshTokenFactory = refreshTokenFactory;
        _discriminatorTagFactory = discriminatorTagFactory;
    }

    public async Task<CreateAccountResult> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var validationResult = AnnotationValidator.TryValidate(request);
        var errorList = validationResult.Error.ToErrorList();

        var userExists = await _accountRepository.GetFirstAsync(new EmailEqualSpecification(request.EmailAddress));
        if (userExists != null)
            errorList.AddError("EmailAddress", "Email already in use.");

        if (errorList.HasErrors)
            return CreateAccountResult.Fail(errorList);

        var passwordHash = await _passwordManager.HashAsync(request.Password);
        var tag = await _discriminatorTagFactory.GenerateDiscriminatorTagAsync(request.Username);
        var user = new UserAccount(request.Username, request.EmailAddress, passwordHash, tag);

        await _accountRepository.InsertAsync(user);
        await _accountRepository.CommitAsync();

        return CreateAccountResult.Ok(user);
    }
}