using System;
using System.Text;
using System.Threading.Tasks;
using Agree.Allow.Domain.Services;
using Agree.Allow.Infrastructure.Configuration;
using Agree.Allow.Infrastructure.Data;
using Agree.Allow.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
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
            // Mail
            var mailConfigSection = configuration.GetSection("MailConfiguration");
            services.Configure<MailConfiguration>(mailConfigSection);

            // Frontend
            var frontendConfigSection = configuration.GetSection("FrontendConfiguration");
            services.Configure<FrontendConfiguration>(frontendConfigSection);

            // JWT
            var tokenConfigSection = configuration.GetSection("TokenConfiguration");
            services.Configure<TokenConfiguration>(tokenConfigSection);

            services
                .AddDbContext<ApplicationDbContext>(
                    opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ITokenService, TokenService>();
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
                            context.Token = context.Request.Cookies["agreeallow_accesstoken"];
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
