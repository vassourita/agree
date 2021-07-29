using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Dtos
{
    public class SearchAccountsDto : Pagination
    {
        public string Query { get; set; }
    }
}