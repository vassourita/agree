using System.Collections.Generic;
using System;
using Agree.Accord.Domain.Identity;

namespace Agree.Accord.Domain.Servers
{
    public class Server
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ServerPrivacy PrivacyLevel { get; set; }

        public ICollection<ApplicationUser> Members { get; set; }
        public ICollection<ServerRole> Roles { get; set; }
    }
}