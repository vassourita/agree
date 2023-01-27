namespace Agree.Allow.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

/// <summary>
/// A factory for creating <see cref="ApplicationDbContext"/> instances during design-time builds.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseNpgsql("Server=localhost;Port=4001;Uid=docker;Pwd=docker;Database=allow_db");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}