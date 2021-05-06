using System;
using Agree.Allow.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Agree.Allow.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public int Tag { get; set; }
        public string DisplayName { get; set; }

        public ApplicationUserViewModel ToViewModel()
            => new ApplicationUserViewModel
            {
                Email = Email,
                Verified = EmailConfirmed,
                Id = Id,
                Tag = Tag.ToString().PadLeft(4, '0'),
                UserName = DisplayName
            };
    }
}