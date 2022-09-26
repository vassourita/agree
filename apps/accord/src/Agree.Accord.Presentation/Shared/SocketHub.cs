namespace Agree.Accord.Presentation.Shared;

using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Requests;
using Agree.Accord.Presentation.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

public class SocketHub : CustomHubBase
{
    public SocketHub(ILogger<CustomHubBase> logger, IMediator mediator)
        : base(logger, mediator) { }
}