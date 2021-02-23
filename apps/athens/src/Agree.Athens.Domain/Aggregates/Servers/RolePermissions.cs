namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class RolePermissions
    {
        public bool CanUpdateServerName { get; set; } = false;
        public bool CanDeleteServer { get; set; } = false;
        public bool CanAddUsers { get; set; } = false;
        public bool CanRemoveUsers { get; set; } = false;
    }
}