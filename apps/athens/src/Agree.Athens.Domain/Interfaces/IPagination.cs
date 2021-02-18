namespace Agree.Athens.Domain.Interfaces
{
    public interface IPagination
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}