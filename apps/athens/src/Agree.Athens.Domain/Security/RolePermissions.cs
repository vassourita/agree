namespace Agree.Athens.Domain.Security
{
    public class RolePermissions
    {
        public bool CanDeleteServer { get; set; }
        public bool CanUpdateServerName { get; set; }
        public bool CanUpdateServerDescription { get; set; }
        public bool CanUpdateServerAvatar { get; set; }
        public bool CanRemoveUsers { get; set; }
        public bool CanCreateNewRoles { get; set; }
        public bool CanDeleteRoles { get; set; }
    }
}