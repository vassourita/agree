namespace Agree.Allow.Infrastructure.IoC;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Agree.Allow.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Agree.SharedKernel.Data;
using MediatR;
using System.Reflection;
using Agree.Allow.Domain;
using Agree.Allow.Domain.Tokens;
using Agree.Allow.Infrastructure.Providers;

/// <summary>
/// Provides extension methods for the <see cref="IServiceCollection"/> interface that configure the application infrastructure and auth.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the application's infrastructure services and domain handlers to the service collection.
    /// </summary>
    public static IServiceCollection AddAllowInfrastructure(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        services.AddMediatR(assembly, typeof(UserAccount).Assembly);

        services.AddScoped<TokenValidator>();
        services.AddScoped<AccessTokenFactory>();
        services.AddScoped<RefreshTokenFactory>();

        services.AddScoped<IPasswordManager, BCryptPasswordManager>();

        // Data
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );
        services.AddTransient(typeof(IRepository<,>), typeof(GenericRepository<,>));

        return services;
    }
}