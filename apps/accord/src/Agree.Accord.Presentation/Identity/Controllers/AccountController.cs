namespace Agree.Accord.Presentation.Identity.Controllers;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Requests;
using Agree.Accord.Presentation.Identity.ViewModels;
using Agree.Accord.Presentation.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A controller for managing user accounts.
/// </summary>
[ApiController]
[Route("api/identity/accounts")]
[Authorize]
public class AccountController : CustomControllerBase
{
    public AccountController(IMediator mediator)
        : base(mediator) { }

    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<UserAccountViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public async Task<IActionResult> Register([FromBody] CreateAccountRequest request)
    {
        var result = await _mediator.Send(request);

        if (result.Failed)
            return BadRequest(new ValidationErrorResponse(result.Error));

        var (account, token) = result.Data;

        Response.Cookies.Append(AccessTokenCookieName, token.Token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        return Created(
            Url.Link("GetAccountById", new { account.Id }),
            new GenericResponse(UserAccountViewModel.FromEntity(account)));
    }

    [HttpGet]
    [Route("@me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<UserAccountViewModel>))]
    public async Task<IActionResult> Me()
    {
        var entity = await _mediator.Send(new GetAccountByIdRequest(CurrentlyLoggedUser.Id));
        return Ok(new GenericResponse(UserAccountViewModel.FromEntity(entity)));
    }

    [HttpGet]
    [Route("{id:guid}", Name = "GetAccountById")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<UserAccountViewModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Show([FromRoute] GetAccountByIdRequest request)
    {
        var entity = await _mediator.Send(request);
        return entity == null ? NotFound() : Ok(new GenericResponse(UserAccountViewModel.FromEntity(entity)));
    }

    [HttpGet]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<UserAccountViewModel>>))]
    public async Task<IActionResult> Index([FromQuery] SearchAccountsRequest search)
    {
        var entities = await _mediator.Send(search);
        return Ok(new GenericResponse(entities.Select(UserAccountViewModel.FromEntity)));
    }
}