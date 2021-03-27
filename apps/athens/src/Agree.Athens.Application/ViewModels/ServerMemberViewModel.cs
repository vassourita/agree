using System.Collections.Generic;
using System;

namespace Agree.Athens.Application.ViewModels
{
    public class ServerMemberViewModel
    {
        public record Role(Guid id);

        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Tag { get; set; }

        public string AvatarUrl { get; set; }

        public string UserNameWithTag { get => $"{UserName}#{Tag}"; }

        public IEnumerable<ServerMemberViewModel.Role> Roles { get; set; }
    }
}