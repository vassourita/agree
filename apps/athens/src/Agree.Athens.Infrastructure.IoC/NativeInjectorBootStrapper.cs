using Agree.Athens.Domain.Interfaces;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Data.EntityFramework;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Agree.Athens.Application.Services;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Infrastructure.Providers;

namespace Agree.Athens.Infrastructure.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Infrastructure - Data - EntityFramework
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(ISoftDeleteRepository<>), typeof(SoftDeleteRepository<>));

            // Infrastructure - Providers
            services.AddScoped<IHashProvider, BCryptHashProvider>();

            // Application
            services.AddScoped<AuthService>();
        }
    }
}