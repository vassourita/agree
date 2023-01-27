namespace Agree.Allow.Presentation.Accounts;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Agree.Allow.Domain.Requests;
using Agree.SharedKernel.Presentation.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A controller for managing user accounts.
/// </summary>
[ApiController]
[Route("api/accounts")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserAccountViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public async Task<IActionResult> Register([FromBody] CreateAccountRequest request)
    {
        var result = await _mediator.Send(request);

        if (result.Failed)
            return BadRequest(new ValidationErrorResponse(result.Error));

        var account = result.Data;

        return Created(
            Url.Link("GetAccountById", new { account.Id }),
            UserAccountViewModel.FromEntity(account));
    }

    [HttpGet]
    [Route("@me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserAccountViewModel))]
    public async Task<IActionResult> Me()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var entity = await _mediator.Send(new GetAccountByIdRequest(userId));
        return Ok(UserAccountViewModel.FromEntity(entity));
    }

    [HttpGet]
    [Route("{id:guid}", Name = "GetAccountById")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserAccountViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Show([FromRoute] GetAccountByIdRequest request)
    {
        var entity = await _mediator.Send(request);
        return entity == null ? NotFound() : Ok(UserAccountViewModel.FromEntity(entity));
    }

    [HttpGet]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserAccountViewModel>))]
    public async Task<IActionResult> Index([FromQuery] SearchAccountsRequest search)
    {
        var entities = await _mediator.Send(search);
        return Ok(entities.Select(UserAccountViewModel.FromEntity));
    }
}