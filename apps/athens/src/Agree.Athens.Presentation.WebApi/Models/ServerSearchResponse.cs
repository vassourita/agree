using System.Collections.Generic;
using System.Linq;
using Agree.Athens.Application.ViewModels;

namespace Agree.Athens.Presentation.WebApi.Models
{
    public class ServerSearchResponse : Response
    {
        public IEnumerable<ServerViewModel> Servers { get; private set; }

        public ServerSearchResponse(IEnumerable<ServerViewModel> servers, string message = null)
            : base(message is null ? $"{servers.Count()} servers found" : message)
        {
            Servers = servers;
        }
    }
}