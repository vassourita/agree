using System;
using Agree.Accord.Domain.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Agree.Accord.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Agree.Accord.SharedKernel.Data;
using Agree.Accord.Domain.Identity.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Server;
using Microsoft.AspNetCore.Identity;
using Agree.Accord.Domain.Providers;
using Agree.Accord.Infrastructure.Providers;
using Agree.Accord.Infrastructure.Configuration;

namespace Agree.Accord.Infrastructure.IoC
{
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
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .LogTo(Console.WriteLine)
            );
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            // Providers
            services.AddScoped<IMailProvider, NativeMailProvider>();

            // Domain Services
            services.AddScoped<TokenService>();
            services.AddScoped<AccountService>();

            return services;
        }

        public static IServiceCollection AddAccordAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
                options.AddPolicy("DefaultCorsPolicy", builder =>
                    builder.WithMethods("GET", "POST", "PATCH", "PUT", "DELETE", "OPTIONS")
                        .WithHeaders(
                            HeaderNames.Accept,
                            HeaderNames.ContentType,
                            HeaderNames.Authorization)
                        .AllowCredentials()
                        .SetIsOriginAllowed(origin =>
                        {
                            if (string.IsNullOrWhiteSpace(origin)) return false;
                            if (origin.ToLower().StartsWith("http://localhost")) return true;
                            return false;
                        })
                )
            );

            services
                .AddDefaultIdentity<ApplicationUser>(SetupIdentityOptions)
                .AddRoles<ServerRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtConfiguration>(options =>
            {
                options.ExpiresInMinutes = 60;
                options.Issuer = configuration["JwtConfiguration:Issuer"];
                options.Audience = configuration["JwtConfiguration:Audience"];
                options.SigningKey = configuration["JwtConfiguration:SigningKey"];
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            return services;
        }

        private static void SetupIdentityOptions(IdentityOptions options)
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;

            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            options.User.RequireUniqueEmail = true;
        }
    }
}