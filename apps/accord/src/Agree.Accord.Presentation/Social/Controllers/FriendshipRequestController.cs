namespace Agree.Accord.Presentation.Social.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Presentation.Responses;
using Agree.Accord.Presentation.Shared;
using Agree.Accord.Presentation.Social.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A controller for managing friendship requests.
/// </summary>
[ApiController]
[Route("api/friendship-requests")]
[Authorize]
public class FriendshipRequestController : CustomControllerBase
{
    public FriendshipRequestController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [Route("sent")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<FriendshipRequestViewModel>>))]
    public async Task<IActionResult> SentRequests()
    {
        var requests = await _mediator.Send(new GetUserSentFriendshipRequestsRequest(await GetAuthenticatedUserAccount()));
        return Ok(new { Requests = requests.Select(FriendshipRequestViewModel.FromEntity) });
    }

    [HttpGet]
    [Route("received")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<FriendshipRequestViewModel>>))]
    public async Task<IActionResult> ReceivedRequests()
    {
        var requests = await _mediator.Send(new GetUserReceivedFriendshipRequestsRequest(await GetAuthenticatedUserAccount()));
        return Ok(new { Requests = requests.Select(FriendshipRequestViewModel.FromEntity) });
    }

    [HttpPost]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public async Task<IActionResult> Send([FromBody] SendFriendshipRequestRequest friendshipRequestRequest)
    {
        friendshipRequestRequest.From = await GetAuthenticatedUserAccount();
        var result = await _mediator.Send(friendshipRequestRequest);
        if (result.Failed)
        {
            return BadRequest(new ValidationErrorResponse(result.Error));
        }

        return Ok();
    }

    [HttpPut]
    [Route("{fromUserId:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public async Task<IActionResult> Accept([FromRoute] Guid fromUserId)
    {
        var result = await _mediator.Send(new AcceptFriendshipRequestRequest(await GetAuthenticatedUserAccount(), fromUserId));
        if (result.Failed)
        {
            return BadRequest(new ValidationErrorResponse(result.Error));
        }

        return Ok();
    }

    [HttpDelete]
    [Route("{fromUserId:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public async Task<IActionResult> Decline([FromRoute] Guid fromUserId)
    {
        var result = await _mediator.Send(new DeclineFriendshipRequestRequest(await GetAuthenticatedUserAccount(), fromUserId));
        if (result.Failed)
        {
            return BadRequest(new ValidationErrorResponse(result.Error));
        }

        // await _hubContext.Clients
        //     .User(fromUserId.ToString())
        //     .SendAsync(
        //         FriendshipHub.FriendshipRequestDeclinedMessage,
        //         FriendshipRequestViewModel.FromEntity(result.Data)
        //     );

        return Ok();
    }
}