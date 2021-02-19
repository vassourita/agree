using Agree.Athens.Domain.Interfaces.Providers;

namespace Agree.Athens.Infrastructure.Providers
{
    public class BCryptHashProvider : IHashProvider
    {
        public bool Compare(string hashed, string s)
        {
            var result = BCrypt.Net.BCrypt.Verify(s, hashed);
            return result;
        }

        public string Hash(string s)
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(s);
            return hashed;
        }
    }
}