namespace Agree.Allow.Domain.Handlers.Queries;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Agree.Allow.Domain;
using Agree.Allow.Domain.Requests;
using Agree.Allow.Domain.Specifications;
using Agree.SharedKernel.Data;
using MediatR;

public class SearchAccountsHandler : IRequestHandler<SearchAccountsRequest, IEnumerable<UserAccount>>
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;

    public SearchAccountsHandler(IRepository<UserAccount, Guid> accountRepository) => _accountRepository = accountRepository;

    public async Task<IEnumerable<UserAccount>> Handle(SearchAccountsRequest request, CancellationToken cancellationToken)
        => await _accountRepository.GetAllAsync(new NameTagLikeSpecification(request.Query, request));
}