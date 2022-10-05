namespace Agree.Accord.Domain.Servers.Handlers.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Accord.Domain.Servers.Requests;
using Agree.Accord.Domain.Servers.Results;
using Agree.Accord.Domain.Servers;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;
using Agree.Accord.Domain.Servers.Specifications;

/// <summary>
/// Handles the creation of a new <see cref="Category"/>.
/// </summary>
public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResult>
{
    private readonly IRepository<Category, Guid> _categoryRepository;
    private readonly IRepository<Server, Guid> _serverRepository;

    public CreateCategoryHandler(IRepository<Category, Guid> categoryRepository, IRepository<Server, Guid> serverRepository)
    {
        _categoryRepository = categoryRepository;
        _serverRepository = serverRepository;
    }

    public async Task<CreateCategoryResult> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var validationResult = AnnotationValidator.TryValidate(request);
        var errors = new ErrorList();

        if (validationResult.Failed)
            errors.AddErrors(validationResult.Error);

        var server = await _serverRepository.GetFirstAsync(new ServerIdEqualSpecification(request.ServerId, request.Requester.Id));

        if (server == null)
            errors.AddError("ServerId", "The server does not exist.");

        if (errors.HasErrors())
            return CreateCategoryResult.Fail(errors);

        var category = new Category(request.Name, server);

        await _categoryRepository.InsertAsync(category);
        await _categoryRepository.CommitAsync();

        return CreateCategoryResult.Ok(category);
    }
}