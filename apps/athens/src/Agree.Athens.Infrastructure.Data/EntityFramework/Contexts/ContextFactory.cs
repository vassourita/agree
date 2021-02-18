using System.Reflection;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using Agree.Athens.Infrastructure.Configuration;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Contexts
{
    public class ContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var configuration = ConfigurationFactory.CreateConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.LogTo(Console.WriteLine);

            return new DataContext(optionsBuilder.Options);
        }
    }
}