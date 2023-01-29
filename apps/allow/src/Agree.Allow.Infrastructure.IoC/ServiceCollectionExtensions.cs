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

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllowInfrastructure(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        var tokenConfig = configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
        var key = Encoding.ASCII.GetBytes(tokenConfig.SigningKey);

        services.Configure<JwtConfiguration>(options =>
        {
            options.AccessTokenExpiresInMinutes = 60;
            options.RefreshTokenExpiresInDays = 30;
            options.Issuer = tokenConfig.Issuer;
            options.SigningKey = tokenConfig.SigningKey;
        });

        services.AddMediatR(assembly, typeof(UserAccount).Assembly);
        services.AddAutoMapper(assembly, typeof(UserAccount).Assembly, typeof(UserAccountDbModel).Assembly);

        services.AddScoped<TokenValidator>();
        services.AddScoped<AccessTokenFactory>();
        services.AddScoped<RefreshTokenFactory>();
        services.AddScoped<DiscriminatorTagFactory>();

        services.AddScoped<IPasswordManager, BCryptPasswordManager>();

        return services;
    }

    public static IServiceCollection AddAllowDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );
        services.AddTransient(typeof(IRepository<UserAccount, Guid>), typeof(GenericRepository<UserAccountDbModel, UserAccount, Guid>));
        services.AddTransient(typeof(IRepository<ClientApplication, Guid>), typeof(GenericRepository<ClientApplicationDbModel, ClientApplication, Guid>));

        return services;
    }
}