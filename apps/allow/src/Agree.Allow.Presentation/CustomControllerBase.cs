namespace Agree.Allow.Presentation;

using System.Net;
using System.Threading.Tasks;
using Agree.Allow.Domain;
using Agree.Allow.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class CustomControllerBase : ControllerBase
{
    protected readonly IMediator _mediator;

    public CustomControllerBase(IMediator mediator) => _mediator = mediator;

    protected UserAccount CurrentlyLoggedUser =>
        HttpContext.User.Identity.IsAuthenticated
        ? UserAccount.FromClaims(HttpContext.User)
        : null;

    protected async Task<UserAccount> GetAuthenticatedUserAccount()
        => await _mediator.Send(new GetAccountByIdRequest(CurrentlyLoggedUser.Id));

    protected IActionResult InternalServerError() => StatusCode((int)HttpStatusCode.InternalServerError);

    protected IActionResult InternalServerError(object obj) => StatusCode((int)HttpStatusCode.InternalServerError, obj);
}