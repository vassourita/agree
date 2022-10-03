namespace Agree.Accord.Presentation.Social.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Servers;
using Agree.Accord.Domain.Servers.Requests;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Presentation.Identity.ViewModels;
using Agree.Accord.Presentation.Responses;
using Agree.Accord.Presentation.Servers.ViewModels;
using Agree.Accord.Presentation.Shared;
using Agree.Accord.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A controller for managing servers.
/// </summary>
[ApiController]
[Route("api/servers")]
[Authorize]
public class ServerController : CustomControllerBase
{
    public ServerController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<Server>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public async Task<IActionResult> Store([FromBody] CreateServerRequest request)
    {
        request.Owner = await GetAuthenticatedUserAccount();
        var result = await _mediator.Send(request);
        if (result.Failed)
            return BadRequest(result.Error);
        return Ok(new GenericResponse(ServerViewModel.FromEntity(result.Data)));
    }

    [HttpGet]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Server>>))]
    public async Task<IActionResult> Index([FromQuery] SearchServersRequest request)
    {
        request.UserId = (await GetAuthenticatedUserAccount()).Id;
        var result = await _mediator.Send(request);
        return Ok(new GenericResponse(result.Select(ServerViewModel.FromEntity)));
    }
}