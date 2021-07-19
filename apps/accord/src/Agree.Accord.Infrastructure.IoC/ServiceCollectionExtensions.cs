using Agree.Accord.Domain.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Agree.Accord.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Agree.Accord.SharedKernel.Data;
using Agree.Accord.Domain.Providers;
using Agree.Accord.Infrastructure.Providers;
using Agree.Accord.Domain.Identity.Tokens;

namespace Agree.Accord.Infrastructure.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccordInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IHashProvider, BCryptHashProvider>();
            services.AddScoped<AccountService>();

            return services;
        }

        public static IServiceCollection AddAccordAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(options =>
            {
                options.ExpiresInMinutes = 60;
                options.Issuer = configuration["JwtConfiguration:Issuer"];
                options.Audience = configuration["JwtConfiguration:Audience"];
                options.SigningKey = configuration["JwtConfiguration:SigningKey"];
            });
            return services;
        }
    }
}