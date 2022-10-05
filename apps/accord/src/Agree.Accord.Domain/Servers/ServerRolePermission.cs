namespace Agree.Accord.Domain.Servers;

public class ServerRolePermission
{
    public ServerRolePermission(ServerResource resource, ServerAction action)
    {
        Resource = resource;
        Action = action;
    }

    public ServerResource Resource { get; private set; }
    public ServerAction Action { get; private set; }


    public enum ServerResource
    {
        Server,
        Category,
        Channel,
        Message,
        Role
    }

    public enum ServerAction
    {
        Create,
        Read,
        Update,
        Delete
    }
}