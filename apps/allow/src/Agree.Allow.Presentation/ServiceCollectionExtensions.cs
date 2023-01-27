using System.Text;
using Agree.Allow.Domain;
using Agree.Allow.Domain.Tokens;
using Agree.SharedKernel.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the authentication and authorization configuration to the application.
    /// </summary>
    public static IServiceCollection AddAllowAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenConfig = configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
        var key = Encoding.ASCII.GetBytes(tokenConfig.SigningKey);

        services.Configure<JwtConfiguration>(options =>
        {
            options.AccessTokenExpiresInMinutes = 60;
            options.RefreshTokenExpiresInDays = 30;
            options.Issuer = tokenConfig.Issuer;
            options.Audiences = tokenConfig.Audiences;
            options.SigningKey = tokenConfig.SigningKey;
        });

        var tokenValidator = services.BuildServiceProvider().GetService<TokenValidator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = tokenConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudiences = tokenConfig.Audiences,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.SigningKey)),
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = async context =>
                    {
                        var accessToken = context.Request.Query["Authorization"][0];
                        if (string.IsNullOrEmpty(accessToken))
                        {
                            context.Fail("Empty token");
                            return;
                        }

                        if (!accessToken.StartsWith("Bearer "))
                        {
                            context.Fail("Invalid token");
                            return;
                        }

                        context.Token = accessToken.Substring("Bearer ".Length).Trim();
                        var account = await tokenValidator.ValidateAsync(context.Token);
                        if (account == null)
                        {
                            context.Fail("Invalid token");
                            return;
                        }

                        context.Principal = account.ToClaimsPrincipal();

                        context.Success();
                    },
                };
            });

        return services;
    }
}