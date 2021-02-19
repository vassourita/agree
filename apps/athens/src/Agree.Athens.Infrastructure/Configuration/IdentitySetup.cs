using System;
using Microsoft.AspNetCore.Identity;

namespace Agree.Athens.Infrastructure.Configuration
{
    public static class IdentitySetup
    {
        public static void Configure(IdentityOptions options)
        {
            options.User.RequireUniqueEmail = true;

            options.SignIn.RequireConfirmedAccount = true;

            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        }
    }
}