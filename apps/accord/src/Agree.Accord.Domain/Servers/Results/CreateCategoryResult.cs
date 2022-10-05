namespace Agree.Accord.Domain.Servers.Results;

using Agree.Accord.Domain.Servers;
using Agree.Accord.SharedKernel;

public class CreateCategoryResult : Result<Category, ErrorList>
{
    private CreateCategoryResult(Category category) : base(category)
    { }
    private CreateCategoryResult(ErrorList error) : base(error)
    { }

    public static CreateCategoryResult Ok(Category category) => new(category);
    public static CreateCategoryResult Fail(ErrorList data) => new(data);
}