namespace Agree.Accord.Presentation.Identity.Controllers;

using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A controller that handles user login and logout operations.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("api/identity/sessions")]
public class SessionController : CustomControllerBase
{
    public SessionController(IMediator mediator)
        : base(mediator) { }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Login(PasswordLoginRequest command)
    {
        var result = await _mediator.Send(command);

        if (result.Failed)
            return BadRequest();

        Response.Cookies.Append(AccessTokenCookieName, result.Data.Token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        return NoContent();
    }

    [HttpDelete]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(AccessTokenCookieName);
        return NoContent();
    }
}