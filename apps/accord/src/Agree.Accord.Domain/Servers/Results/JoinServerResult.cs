namespace Agree.Accord.Domain.Servers.Results;

using Agree.Accord.SharedKernel;

/// <summary>
/// The result of joining a server.
/// </summary>
public class JoinServerResult : Result<ServerMember, ErrorList>
{
    private JoinServerResult(ServerMember serverMember) : base(serverMember)
    { }
    private JoinServerResult(ErrorList error) : base(error)
    { }

    public static JoinServerResult Ok(ServerMember serverMember) => new(serverMember);
    public static JoinServerResult Fail(ErrorList data) => new(data);
}