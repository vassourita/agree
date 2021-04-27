using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Agree.Athens.Application.Mappings;
using Agree.Athens.Application.Security;
using Agree.Athens.Application.Services;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Services;
using Agree.Athens.Infrastructure.Configuration;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Agree.Athens.Infrastructure.Data.EntityFramework.DataModels;
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
                config.AddProfile(new DomainEntityToViewModelProfile());
            });

            // Infrastructure - Data - EntityFramework
            services.AddDbContext<DataContext>(options =>
                options
                    .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                    .LogTo(Console.WriteLine));
            services.AddScoped<IAccountRepository, EFAccountRepository>();
            services.AddScoped<IServerRepository, EFServerRepository>();
            services.AddScoped<ITokenRepository, EFTokenRepository>();

            // Infrastructure - Configuration
            var mailConfiguration = configuration.GetSection("MailConfiguration");
            services.Configure<MailConfiguration>(mailConfiguration);
            services.Configure<HashConfiguration>(configuration.GetSection("HashConfiguration"));
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
            services.Configure<AzuriteStorageConfiguration>(configuration.GetSection("AzuriteStorageConfiguration"));
            services.Configure<UIConfiguration>(configuration.GetSection("UIConfiguration"));

            // Infrastructure - Providers
            services.AddScoped<IHashProvider, BcryptHashProvider>();
            services.AddScoped<IMailProvider, FluentMailProvider>();
            services.AddScoped<IMailTemplateProvider, FluentMailTemplateProvider>();
            services.AddScoped<IFileStorageProvider, AzuriteStorageProvider>();

            // Domain/Application Services
            services.AddScoped<MailService>();
            services.AddScoped<AccountService>();
            services.AddScoped<UserAccountService>();
            services.AddScoped<TokenService>();
            services.AddScoped<AvatarService>();
            services.AddScoped<ServerService>();

            // Libs
            services.AddFluentEmail("agree@vassourita.com", "Vinicius Vassão")
                .AddSmtpSender(() =>
                {
                    var host = mailConfiguration.GetValue<string>("Host");
                    var port = mailConfiguration.GetValue<int>("Port");
                    var userName = mailConfiguration.GetValue<string>("UserName");
                    var password = mailConfiguration.GetValue<string>("Password");
                    var enableSsl = mailConfiguration.GetValue<bool>("EnableSsl");

                    return new SmtpClient(host, port)
                    {
                        Credentials = new NetworkCredential(userName, password),
                        EnableSsl = enableSsl
                    };
                })
                .AddRazorRenderer();
        }
    }
}