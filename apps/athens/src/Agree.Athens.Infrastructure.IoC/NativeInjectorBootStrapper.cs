using Agree.Athens.Domain.Interfaces;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Identity;
using Agree.Athens.Infrastructure.Data.EntityFramework;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Agree.Athens.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

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

            // Infrastructure - Identity
            services.AddDbContext<IdentityContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<ApplicationUser>(IdentitySetup.Configure)
                .AddEntityFrameworkStores<IdentityContext>();
        }
    }
}