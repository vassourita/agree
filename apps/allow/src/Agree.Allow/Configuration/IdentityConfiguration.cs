using Microsoft.AspNetCore.Identity;

namespace Agree.Allow.Presentation.Configuration
{
    public static class IdentityConfiguration
    {
        public static void Setup(IdentityOptions options)
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 7;

            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            options.Tokens.AuthenticatorIssuer = "agree-allow";

            options.User.RequireUniqueEmail = true;
        }
    }
}