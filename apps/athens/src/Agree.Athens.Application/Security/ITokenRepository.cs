using System;
using System.Threading.Tasks;

namespace Agree.Athens.Application.Security
{
    public interface ITokenRepository
    {
        Task<RefreshToken> GetAsync(string tokenValue, Guid userId);
        Task<RefreshToken> AddAsync(RefreshToken token);
    }
}