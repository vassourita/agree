namespace Agree.Accord.Presentation;

using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Presentation.Identity.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

/// <summary>
/// A custom <see cref="Hub"/> that provides useful and common methods for all hubs.
/// </summary>
public class CustomHubBase : Hub
{
    public const string AccessTokenCookieName = "agreeaccord_accesstoken";

    protected readonly AccountService _accountService;
    private readonly ILogger<CustomHubBase> _logger;

    public CustomHubBase(AccountService accountService, ILogger<CustomHubBase> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    protected ApplicationUserViewModel CurrentlyLoggedUser =>
        Context.User.Identity.IsAuthenticated
        ? ApplicationUserViewModel.FromClaims(Context.User)
        : null;

    protected async Task<ApplicationUser> GetAuthenticatedUserAccount()
        => await _accountService.GetAccountByIdAsync(CurrentlyLoggedUser.Id);

    public string GetConnectionId() => Context.ConnectionId;

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        _logger.LogInformation("New connection on hub: {0}", CurrentlyLoggedUser.NameTag);
    }
}