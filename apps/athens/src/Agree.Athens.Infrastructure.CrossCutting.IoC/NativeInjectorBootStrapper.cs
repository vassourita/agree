using Agree.Athens.Application.Services;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Interfaces.Services;
using Agree.Athens.Domain.Services;
using Agree.Athens.Infrastructure.Configuration;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.Repositories;
using Agree.Athens.Infrastructure.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agree.Athens.Infrastructure.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Infrastructure - Data - EntityFramework
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAccountRepository, EFAccountRepository>();

            // Infrastructure - Configuration
            services.Configure<HashConfiguration>(configuration.GetSection("HashConfiguration"));
            services.Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"));

            // Infrastructure - Providers
            services.AddScoped<IHashProvider, BcryptHashProvider>();
            services.AddScoped<IMailProvider, NativeMailProvider>();

            // Domain - Services
            services.AddScoped<AccountService>();
            services.AddScoped<IMailService, MailService>();
        }
    }
}