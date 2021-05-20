using System.Threading.Tasks;

namespace Agree.Allow.Domain.Services
{
    public interface ITagService
    {
        Task<int> GenerateTag(string userName);
    }
}