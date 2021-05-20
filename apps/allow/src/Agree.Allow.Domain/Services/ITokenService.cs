using System.Threading.Tasks;

namespace Agree.Allow.Domain.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(string userEmail);
    }
}