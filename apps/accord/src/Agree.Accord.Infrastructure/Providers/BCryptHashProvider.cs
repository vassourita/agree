using System.Threading.Tasks;
using Agree.Accord.Domain.Providers;

namespace Agree.Accord.Infrastructure.Providers
{
    public class BCryptHashProvider : IHashProvider
    {
        public Task<bool> CompareAsync(string hashed, string s)
        {
            return Task.Run(() => BCrypt.Net.BCrypt.Verify(s, hashed));
        }

        public Task<string> HashAsync(string s)
        {
            return Task.Run(() => BCrypt.Net.BCrypt.HashPassword(s));
        }
    }
}