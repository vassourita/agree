using System;
using Microsoft.AspNetCore.Identity;

namespace Agree.Accord.Domain.Server
{
    public class ServerRole : IdentityRole<Guid>
    {
        public Guid ServerId { get; set; }
    }
}