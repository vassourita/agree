using System.Security.Claims;
using System;
using Agree.Accord.Domain.Identity;
using System.Linq;
using Agree.Accord.Domain.Servers;

namespace Agree.Accord.Presentation.Servers.ViewModels
{
    public class ServerViewModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string PrivacyLevel { get; private set; }

        public static ServerViewModel FromEntity(Server entity)
        {
            return new ServerViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                PrivacyLevel = entity.PrivacyLevel.ToString()
            };
        }
    }
}