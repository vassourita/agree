using Agree.Accord.Domain.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Agree.Accord.Infrastructure.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccordInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<AccountService>();

            return services;
        }

        public static IServiceCollection AddAccordAuthentication(this IServiceCollection services)
        {

            return services;
        }
    }
}