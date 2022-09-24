namespace Agree.Accord.Domain.Identity.Dtos;

using Agree.Accord.SharedKernel.Data;

public class SearchAccountsDto : Pagination
{
    public string Query { get; set; }
}