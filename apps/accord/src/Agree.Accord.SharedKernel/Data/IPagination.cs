namespace Agree.Accord.SharedKernel.Data
{
    public interface IPagination
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }

    public class Pagination : IPagination
    {
        public int Page { get => _page == default(int) ? 1 : _page; set => _page = value; }
        private int _page { get; set; }
        public int PageSize { get => _pageSize == default(int) ? 20 : _pageSize; set => _pageSize = value; }
        private int _pageSize { get; set; }
    }
}