using System;
using Microsoft.AspNetCore.Identity;

namespace Agree.Accord.Domain.Servers
{
    public class ServerRole : IdentityRole<Guid>
    {
        public Guid ServerId { get; set; }
        public Server Server { get; set; }
    }
}