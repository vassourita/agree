namespace Agree.Accord.SharedKernel.Data;

using System.ComponentModel.DataAnnotations;

public interface IPagination
{
    int Page { get; set; }
    int PageSize { get; set; }
}

public class Pagination : IPagination
{
    public int Page { get => _page == default ? 1 : _page; set => _page = value; }
    private int _page { get; set; }

    [Range(1, 100)]
    public int PageSize { get => _pageSize == default ? 20 : _pageSize; set => _pageSize = value; }
    private int _pageSize { get; set; }
}