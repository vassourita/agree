using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Agree.Athens.Infrastructure.Providers
{
    public class BcryptHashProvider : IHashProvider
    {
        private readonly HashConfiguration _hashConfiguration;

        public BcryptHashProvider(IOptions<HashConfiguration> hashConfiguration)
        {
            _hashConfiguration = hashConfiguration.Value;
        }

        public bool Compare(string s, string hashed)
        {
            var result = BCrypt.Net.BCrypt.Verify(s, hashed);
            return result;
        }

        public string Hash(string s)
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(s, _hashConfiguration.WorkFactor);
            return hashed;
        }
    }
}