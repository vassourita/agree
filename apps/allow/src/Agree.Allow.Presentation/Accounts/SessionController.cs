namespace Agree.Allow.Presentation.Accounts;

using System.Threading.Tasks;
using Agree.Allow.Domain.Requests;
using Agree.SharedKernel;
using Agree.SharedKernel.Presentation.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Store([FromBody] PasswordLoginRequest body, [FromQuery] string grantType, [FromQuery] string refreshToken)
    {
        IAuthenticationRequest command = grantType switch
        {
            "password" => body,
            "refresh_token" => new RefreshTokenRequest(refreshToken),
            _ => null
        };

        if (command == null)
            return BadRequest(new ValidationErrorResponse(new ErrorList("grant_type", "Invalid grant type")));

        var result = await _mediator.Send(command);

        if (result.Failed)
            return BadRequest();

        return Ok(new AuthenticationViewModel(result.Data));
    }
}