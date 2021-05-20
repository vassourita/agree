using System;
using System.Text;
using Agree.Allow.Domain.Services;
using Agree.Allow.Infrastructure.Configuration;
using Agree.Allow.Infrastructure.Data;
using Agree.Allow.Infrastructure.Mappings;
using Agree.Allow.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Agree.Allow.Infrastructure.IoC
{
    public static class NativeContainerBootStrapper
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationDbContext>(
                    opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IMailService, MailService>();

            // Mail
            var mailConfigSection = configuration.GetSection("MailConfiguration");
            services.Configure<MailConfiguration>(mailConfigSection);

            // Frontend
            var frontendConfigSection = configuration.GetSection("FrontendConfiguration");
            services.Configure<FrontendConfiguration>(frontendConfigSection);

            // JWT
            var tokenConfigSection = configuration.GetSection("TokenConfiguration");
            services.Configure<TokenConfiguration>(tokenConfigSection);
        }

        public static void ConfigureIdentity(IdentityOptions options)
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

        public static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigSection = configuration.GetSection("TokenConfiguration");
            var tokenConfig = tokenConfigSection.Get<TokenConfiguration>();
            var key = Encoding.ASCII.GetBytes(tokenConfig.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = tokenConfig.Audience,
                    ValidIssuer = tokenConfig.Issuer,
                };
            });
        }
    }
}
