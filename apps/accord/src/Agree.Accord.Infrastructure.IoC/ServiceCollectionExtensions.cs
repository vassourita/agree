using Agree.Accord.Domain.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Agree.Accord.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Agree.Accord.Infrastructure.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccordInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddScoped<AccountService>();

            return services;
        }

        public static IServiceCollection AddAccordAuthentication(this IServiceCollection services)
        {

            return services;
        }
    }
}