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

namespace Agree.Accord.Presentation
{
    /// <summary>
    /// A custom <see cref="ControllerBase"/> that provides useful and common methods for all controllers.
    /// </summary>
    public class CustomControllerBase : ControllerBase
    {
        public const string AccessTokenCookieName = "agreeaccord_accesstoken";

        protected readonly AccountService _accountService;

        public CustomControllerBase(AccountService accountService)
        {
            _accountService = accountService;
        }

        protected ApplicationUserViewModel CurrentlyLoggedUser =>
            HttpContext.User.Identity.IsAuthenticated
            ? ApplicationUserViewModel.FromClaims(HttpContext.User)
            : null;

        protected async Task<ApplicationUser> GetAuthenticatedUserAccount()
            => await _accountService.GetAccountByIdAsync(CurrentlyLoggedUser.Id);

        protected IActionResult InternalServerError()
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        protected IActionResult InternalServerError(object obj)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, obj);
        }
    }
}