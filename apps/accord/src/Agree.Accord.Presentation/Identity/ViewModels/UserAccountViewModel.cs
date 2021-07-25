using System.Security.Claims;
using System;
using Agree.Accord.Domain.Identity;
using System.Linq;

namespace Agree.Accord.Presentation.Identity.ViewModels
{
    public class ApplicationUserViewModel
    {
        public Guid Id { get; private set; }
        public string DisplayName { get; private set; }
        public string NameTag { get => $"{DisplayName}#{Tag.ToString()}"; }
        public string Tag { get; private set; }
        public bool Verified { get; private set; }

        public static ApplicationUserViewModel FromEntity(ApplicationUser entity)
        {
            return new ApplicationUserViewModel
            {
                Id = entity.Id,
                DisplayName = entity.DisplayName,
                Tag = entity.Tag.ToString(),
                Verified = entity.EmailConfirmed
            };
        }

        public static ApplicationUserViewModel FromClaims(ClaimsPrincipal principal)
        {
            var nameTag = principal.Identity.Name;
            return new ApplicationUserViewModel
            {
                Id = Guid.Parse(principal.Claims.First(c => c.Type == "id").Value),
                DisplayName = nameTag.Split('#').First(),
                Tag = nameTag.Split('#').Last(),
                Verified = bool.Parse(principal.Claims.First(c => c.Type == "verified").Value)
            };
        }
    }
}