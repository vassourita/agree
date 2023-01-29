namespace Agree.Allow.Presentation.Accounts.Controllers;

using System.Threading.Tasks;
using Agree.Allow.Domain.Requests;
using Agree.Allow.Presentation.Accounts.ViewModels;
using Agree.Allow.Presentation.Exceptions;
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
public class SessionController : CustomControllerBase
{
    public SessionController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public async Task<IActionResult> Store(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] PasswordLoginRequest? body,
        [FromQuery(Name = "grant_type")] string grantType,
        [FromQuery(Name = "refresh_token")] string refreshToken)
    {
        try
        {
            IAuthenticationRequest command = grantType.Trim() switch
            {
                "password" => body switch
                {
                    null => throw new EmptyBodyException(),
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
        catch (EmptyBodyException ex)
        {
            return BadRequest(new ValidationErrorResponse(ex.Errors));
        }
        catch (InvalidGrantTypeException ex)
        {
            return BadRequest(new ValidationErrorResponse(ex.Errors));
        }
    }
}