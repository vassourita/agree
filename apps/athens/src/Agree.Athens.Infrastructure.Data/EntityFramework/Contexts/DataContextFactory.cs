using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Contexts
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            var connectionString = "Server=localhost;Port=4001;Uid=docker;Pwd=docker;Database=athens";

            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.LogTo(Console.WriteLine);

            return new DataContext(optionsBuilder.Options);
        }
    }
}