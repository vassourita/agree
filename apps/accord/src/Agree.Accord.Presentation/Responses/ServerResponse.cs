using Agree.Accord.Presentation.Identity.ViewModels;
using Agree.Accord.Presentation.Servers.ViewModels;

namespace Agree.Accord.Presentation.Responses
{
    /// <summary>
    /// The response to a server request.
    /// </summary>
    public class ServerResponse
    {
        public ServerResponse(ServerViewModel server)
        {
            Server = server;
        }

        public ServerViewModel Server { get; private set; }
    }
}