using System;
using System.Collections.Generic;

namespace Agree.Athens.Application.ViewModels
{
    public class ServerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<AccountViewModel> Members { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}