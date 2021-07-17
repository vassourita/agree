using System.Threading.Tasks;

namespace Agree.Accord.Domain.Providers
{
    public interface IHashProvider
    {
        Task<string> HashAsync(string s);
        Task<bool> CompareAsync(string hashed, string s);
    }
}