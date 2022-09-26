namespace Agree.Accord.Presentation.Shared;

using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Requests;
using Agree.Accord.Presentation.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

/// <summary>
/// A custom <see cref="Hub"/> that provides useful and common methods for all hubs.
/// </summary>
public class CustomHubBase : Hub
{
    public const string AccessTokenCookieName = "agreeaccord_accesstoken";

    private readonly ILogger<CustomHubBase> _logger;

    protected readonly IMediator _mediator;

    public CustomHubBase(ILogger<CustomHubBase> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    protected UserAccountViewModel CurrentlyLoggedUser =>
        Context.User.Identity.IsAuthenticated
        ? UserAccountViewModel.FromClaims(Context.User)
        : null;

    protected async Task<UserAccount> GetAuthenticatedUserAccount()
        => await _mediator.Send(new GetAccountByIdRequest(CurrentlyLoggedUser.Id));

    public string GetConnectionId() => Context.ConnectionId;

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        _logger.LogInformation("New connection on hub: {0}", CurrentlyLoggedUser.NameTag);
    }
}