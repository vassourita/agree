using System;
using System.Threading.Tasks;

namespace Agree.Athens.Application.Security
{
    public interface ITokenRepository
    {
        Task<RefreshToken> GetAsync(string tokenValue);
        Task<RefreshToken> AddAsync(RefreshToken token);
    }
}