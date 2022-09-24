namespace Agree.Accord.Presentation.Identity.Controllers;

using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Commands;
using Agree.Accord.Domain.Identity.Services;
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
    private readonly TokenService _tokenService;
    private readonly IMediator _mediator;

    public SessionController(
        TokenService tokenService,
        AccountService accountService) : base(accountService) => _tokenService = tokenService;

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Login(LoginCommand command)
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
    public IActionResult Logout()
    {
        Response.Cookies.Delete(AccessTokenCookieName);
        return NoContent();
    }
}