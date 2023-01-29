namespace Agree.Allow.Domain;

using Agree.SharedKernel;

public class ClientApplication : IEntity<Guid>
{
    public ClientApplication(string name, string audienceName)
    {
        Id = Guid.NewGuid();
        Name = name;
        AudienceName = audienceName;
        AccessKey = Guid.NewGuid().ToString("N");
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string AudienceName { get; private set; }
    public string AccessKey { get; private set; }
}