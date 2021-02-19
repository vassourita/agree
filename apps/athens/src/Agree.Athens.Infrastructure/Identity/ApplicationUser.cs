using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Agree.Athens.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}