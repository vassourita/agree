namespace Agree.Accord.Domain.Identity.Handlers.Queries;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Requests;
using Agree.Accord.Domain.Identity.Specifications;
using Agree.Accord.SharedKernel.Data;
using MediatR;

/// <summary>
/// Handles the retrieval of a <see cref="UserAccount"/> by its id.
/// </summary>
public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdRequest, UserAccount>
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;

    public GetAccountByIdHandler(IRepository<UserAccount, Guid> accountRepository) => _accountRepository = accountRepository;

    public async Task<UserAccount> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
        => await _accountRepository.GetFirstAsync(new UserIdEqualSpecification(request.Id));
}