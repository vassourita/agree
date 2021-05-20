using System;
using Microsoft.AspNetCore.Identity;

namespace Agree.Allow.Domain.Security
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public int Tag { get; set; }
        public string DisplayName { get; set; }
    }
}