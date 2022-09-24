namespace Agree.Accord.Presentation.Responses;

using Agree.Accord.Presentation.Servers.ViewModels;

/// <summary>
/// The response to a server request.
/// </summary>
public class ServerResponse
{
    public ServerResponse(ServerViewModel server) => Server = server;

    public ServerViewModel Server { get; private set; }
}