namespace Agree.Accord.Infrastructure.IoC;

using Agree.Accord.Domain.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Agree.Accord.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Agree.Accord.SharedKernel.Data;
using Agree.Accord.Domain.Identity.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Providers;
using Agree.Accord.Infrastructure.Providers;
using Agree.Accord.Infrastructure.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Agree.Accord.Domain.Servers.Services;
using Agree.Accord.Domain.Social;
using Agree.Accord.Domain.Social.Services;

/// <summary>
/// Provides extension methods for the <see cref="IServiceCollection"/> interface that configure the application infrastructure and auth.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccordInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Options
        services.Configure<NativeMailProviderOptions>(options =>
        {
            options.Host = configuration["Providers:NativeMailProvider:Host"];
            options.Port = int.Parse(configuration["Providers:NativeMailProvider:Port"]);
            options.Password = configuration["Providers:NativeMailProvider:Password"];
            options.UserName = configuration["Providers:NativeMailProvider:UserName"];
        });

        // Data
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        // .LogTo(Console.WriteLine)
        );
        services.AddTransient<IRepository<Friendship>, FriendshipRepository>();
        services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

        // Providers
        services.AddScoped<IMailProvider, NativeMailProvider>();

        // Domain Services
        services.AddScoped<TokenService>();
        services.AddScoped<AccountService>();
        services.AddScoped<ServerService>();
        services.AddScoped<SocialService>();
        services.AddScoped<DirectMessageService>();

        return services;
    }

    public static IServiceCollection AddAccordAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenConfig = configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
        var key = Encoding.ASCII.GetBytes(tokenConfig.SigningKey);

        services.Configure<JwtConfiguration>(options =>
        {
            options.ExpiresInMinutes = 60;
            options.Issuer = tokenConfig.Issuer;
            options.Audience = tokenConfig.Audience;
            options.SigningKey = tokenConfig.SigningKey;
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = tokenConfig.Audience,
                    ValidIssuer = tokenConfig.Issuer,
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var cookieToken = context.Request.Cookies["agreeaccord_accesstoken"];
                        var headerToken = context.Request.Headers["agreeaccord_accesstoken"];
                        context.Token = string.IsNullOrEmpty(cookieToken) ? context.Token : cookieToken;
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}