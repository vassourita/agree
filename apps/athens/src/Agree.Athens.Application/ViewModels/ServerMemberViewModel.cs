using System.Collections.Generic;
using System;

namespace Agree.Athens.Application.ViewModels
{
    public class ServerMemberViewModel : AccountViewModel
    {
        public record Role(Guid id);

        public IEnumerable<ServerMemberViewModel.Role> Roles { get; set; }
    }
}