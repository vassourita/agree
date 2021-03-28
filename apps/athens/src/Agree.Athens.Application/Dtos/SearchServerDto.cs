using Agree.Athens.SharedKernel.Data;

namespace Agree.Athens.Application.Dtos
{
    public class SearchServerDto : Paginated
    {
        public string Query { get; set; } = "";

        public string OrderBy { get; set; } = "";
    }
}