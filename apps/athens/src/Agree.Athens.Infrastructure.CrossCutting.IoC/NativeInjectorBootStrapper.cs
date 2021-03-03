using System.Text;
using Agree.Athens.Application.Services;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Services;
using Agree.Athens.Infrastructure.Configuration;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.Mappings;
using Agree.Athens.Infrastructure.Data.EntityFramework.Repositories;
using Agree.Athens.Infrastructure.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Agree.Athens.Infrastructure.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtConfiguration:Key"))
                    ),
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetValue<string>("JwtConfiguration:Issuer")
                };
            });

            // AutoMapper
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new DbModelToDomainEntityProfile());
            });

            // Infrastructure - Data - EntityFramework
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAccountRepository, EFAccountRepository>();

            // Infrastructure - Configuration
            services.Configure<HashConfiguration>(configuration.GetSection("HashConfiguration"));
            services.Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"));
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));

            // Infrastructure - Providers
            services.AddScoped<IHashProvider, BcryptHashProvider>();
            services.AddScoped<IMailProvider, NativeMailProvider>();

            // Domain/Application Services
            services.AddScoped<MailService>();
            services.AddScoped<AccountService>();
            services.AddScoped<UserAccountService>();
            services.AddScoped<TokenService>();
        }
    }
}