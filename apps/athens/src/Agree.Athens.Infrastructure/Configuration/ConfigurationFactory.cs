using System.IO;
using Microsoft.Extensions.Configuration;

namespace Agree.Athens.Infrastructure.Configuration
{
    public static class ConfigurationFactory
    {
        public static IConfigurationRoot CreateConfiguration()
        {
            var configurationFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configurationFilePath, reloadOnChange: false, optional: false)
                .Build();

            return configuration;
        }
    }
}