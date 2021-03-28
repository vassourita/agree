using Agree.Athens.SharedKernel;
using Agree.Athens.SharedKernel.Data;

namespace Agree.Athens.Domain.Dtos
{
    public class SearchServerDto : Validatable, IPaginated
    {
        public string Query { get; set; } = "";

        public string OrderBy { get; set; } = "";

        public int Page { get; set; } = 1;

        public int PageLimit { get; set; } = 15;
    }
}