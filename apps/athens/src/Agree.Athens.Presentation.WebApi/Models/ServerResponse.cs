using Agree.Athens.Application.ViewModels;

namespace Agree.Athens.Presentation.WebApi.Models
{
    public class ServerResponse : Response
    {
        public ServerViewModel Server { get; private set; }

        public ServerResponse(ServerViewModel server, string message)
            : base(message)
        {
            Server = server;
        }
    }
}