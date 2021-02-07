using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Contexts
{
    public class ContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("../../appsettings.json", reloadOnChange: false, optional: false)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Server=localhost;Port=5001;Uid=docker;Pwd=docker;Database=athens";
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.LogTo(Console.WriteLine);
            return new DataContext(optionsBuilder.Options);
        }
    }
}