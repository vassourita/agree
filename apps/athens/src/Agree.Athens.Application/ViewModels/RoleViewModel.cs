using Agree.Athens.Domain.Aggregates.Servers;

namespace Agree.Athens.Application.ViewModels
{
    public class RoleViewModel
    {
        public string Name { get; set; }
        public string ColorHex { get; set; }
        public RolePermissions Permissions { get; set; }
    }
}