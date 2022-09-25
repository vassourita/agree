namespace Agree.Accord.Presentation;

using System.Net;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Requests;
using Agree.Accord.Presentation.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A custom <see cref="ControllerBase"/> that provides useful and common methods for all controllers.
/// </summary>
public class CustomControllerBase : ControllerBase
{
    public const string AccessTokenCookieName = "agreeaccord_accesstoken";
    protected readonly IMediator _mediator;

    public CustomControllerBase(IMediator mediator) => _mediator = mediator;

    protected UserAccountViewModel CurrentlyLoggedUser =>
        HttpContext.User.Identity.IsAuthenticated
        ? UserAccountViewModel.FromClaims(HttpContext.User)
        : null;

    protected async Task<UserAccount> GetAuthenticatedUserAccount()
        => await _mediator.Send(new GetAccountByIdRequest(CurrentlyLoggedUser.Id));

    protected IActionResult InternalServerError() => StatusCode((int)HttpStatusCode.InternalServerError);

    protected IActionResult InternalServerError(object obj) => StatusCode((int)HttpStatusCode.InternalServerError, obj);
}