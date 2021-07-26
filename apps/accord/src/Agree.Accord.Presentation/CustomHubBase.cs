using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Presentation.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Agree.Accord.Presentation
{
    /// <summary>
    /// A custom <see cref="Hub"/> that provides useful and common methods for all hubs.
    /// </summary>
    public class CustomHubBase : Hub
    {
        public const string AccessTokenCookieName = "agreeaccord_accesstoken";

        protected readonly AccountService _accountService;

        public CustomHubBase(AccountService accountService)
        {
            _accountService = accountService;
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
            Console.WriteLine($"Connected: {CurrentlyLoggedUser.NameTag}");
        }
    }
}