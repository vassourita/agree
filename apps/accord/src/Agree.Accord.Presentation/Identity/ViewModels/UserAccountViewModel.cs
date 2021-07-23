using System;
using Agree.Accord.Domain.Identity;

namespace Agree.Accord.Presentation.ViewModels
{
    public class ApplicationUserViewModel
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string NameTag { get => $"{UserName}#{Tag.ToString()}"; }
        public string Email { get; private set; }
        public string Tag { get; private set; }

        public static ApplicationUserViewModel FromEntity(ApplicationUser entity)
        {
            return new ApplicationUserViewModel
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Email = entity.Email,
                Tag = entity.Tag.ToString()
            };
        }
    }
}