namespace Agree.Athens.SharedKernel.Data
{
    public interface IPaginated
    {
        int Page { get; set; }
        int PageLimit { get; set; }
    }
}