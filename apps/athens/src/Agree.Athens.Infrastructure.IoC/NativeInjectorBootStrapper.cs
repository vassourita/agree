using Agree.Athens.Domain.Interfaces;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Infrastructure.Identity;
using Agree.Athens.Infrastructure.Data.EntityFramework;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Agree.Athens.Infrastructure.Configuration.JwtAuthSampleAPI.Configuration;
using MediatR;
using System;
using Agree.Athens.Domain.Commands.Auth.CreateAccount;

namespace Agree.Athens.Infrastructure.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Infrastructure - Data
            services.AddDbContext<DataContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(ISoftDeleteRepository<>), typeof(SoftDeleteRepository<>));

            // Infrastructure - Identity
            services.AddDbContext<IdentityContext>();
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<IdentityContext>();

            // Domain - Command
            services.AddScoped<IRequestHandler<CreateAccountCommand, Guid>, CreateAccountHandler>();
        }
    }
}