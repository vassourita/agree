using System;
using System.Collections.Generic;
using Agree.Athens.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Agree.Athens.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}