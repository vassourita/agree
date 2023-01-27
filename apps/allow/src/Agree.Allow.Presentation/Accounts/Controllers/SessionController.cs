namespace Agree.Allow.Presentation.Accounts.Controllers;

using System.Threading.Tasks;
using Agree.Allow.Domain.Requests;
using Agree.Allow.Presentation.Accounts.ViewModels;
using Agree.Allow.Presentation.Exceptions;
using Agree.SharedKernel;
using Agree.SharedKernel.Presentation.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// A controller that handles user login and logout operations.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("api/sessions")]
public class SessionController : ControllerBase
{
    private readonly IMediator _mediator;

    public SessionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public async Task<IActionResult> Store(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] PasswordLoginRequest? body,
        [FromQuery] string grantType,
        [FromQuery] string refreshToken)
    {
        IAuthenticationRequest command = grantType.Trim() switch
        {
            "password" => body switch
            {
                null => throw new EmptyBodyException(nameof(body)),
                _ => body
            },
            "refresh_token" => new RefreshTokenRequest(refreshToken),
            null or "" => throw new InvalidGrantTypeException(),
            _ => throw new InvalidGrantTypeException(grantType)
        };

        var result = await _mediator.Send(command);

        if (result.Failed)
            return BadRequest();

        return Ok(new AuthenticationViewModel(result.Data));
    }
}