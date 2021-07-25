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
using Agree.Accord.Domain.Servers;
using Microsoft.AspNetCore.Identity;
using Agree.Accord.Domain.Providers;
using Agree.Accord.Infrastructure.Providers;
using Agree.Accord.Infrastructure.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Agree.Accord.Domain.Servers.Services;

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
            // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            // .LogTo(Console.WriteLine)
            );
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            // Providers
            services.AddScoped<IMailProvider, NativeMailProvider>();

            // Domain Services
            services.AddScoped<TokenService>();
            services.AddScoped<AccountService>();
            services.AddScoped<ServerService>();

            return services;
        }

        public static IServiceCollection AddAccordAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDefaultIdentity<ApplicationUser>(SetupIdentityOptions)
                .AddRoles<ServerRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

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
                            // var requestExternalServiceToken = context.Request.Headers["agreeallow_externaltoken"];
                            // var validExternalServiceToken = configuration["ExternalServiceConfiguration:Token"];
                            // if (requestExternalServiceToken == validExternalServiceToken)
                            // {
                            //     context.Token = context.Request.Headers["agreeallow_accesstoken"];
                            //     return Task.CompletedTask;
                            // }
                            context.Token = context.Request.Cookies["agreeaccord_accesstoken"];
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        private static void SetupIdentityOptions(IdentityOptions options)
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
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