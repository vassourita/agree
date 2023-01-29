namespace Agree.Allow.Presentation;

using System.Text;
using Agree.Allow.Domain.Requests;
using Agree.Allow.Domain.Tokens;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class AuthenticationExtensions
{
    private static async Task OnMessageReceived(MessageReceivedContext context, IMediator mediator)
    {
        var accessKey = context.Request.Headers["AppKey"].ToString();
        if (string.IsNullOrEmpty(accessKey))
        {
            context.Fail("Empty app key");
            return;
        }

        var application = await mediator.Send(new GetClientApplicationByAccessKeyRequest(accessKey));
        if (application == null)
        {
            context.Fail("Invalid app key");
            return;
        }

        var accessToken = context.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(accessToken))
        {
            context.Fail("Empty token");
            return;
        }

        if (!accessToken.StartsWith("Bearer "))
        {
            context.Fail("Invalid token format");
            return;
        }

        context.Token = accessToken.Substring("Bearer ".Length).Trim();
        var tokenValidationResult = await mediator.Send(new ValidateTokenRequest(context.Token));
        if (tokenValidationResult.Failed)
        {
            context.Fail("Invalid token");
            return;
        }

        context.Principal = tokenValidationResult.Data.ToClaimsPrincipal();
        context.Properties.Items["client_id"] = application.Id.ToString();

        context.Success();
    }

    public static IServiceCollection AddAllowAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var mediator = services.BuildServiceProvider().GetService<IMediator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context => OnMessageReceived(context, mediator),
                };
            });

        return services;
    }
}