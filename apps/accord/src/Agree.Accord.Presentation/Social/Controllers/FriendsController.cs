namespace Agree.Accord.Presentation.Social.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.Presentation.Identity.ViewModels;
using Agree.Accord.Presentation.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A controller for managing friends.
/// </summary>
[ApiController]
[Route("api/friends")]
[Authorize]
public class FriendsController : CustomControllerBase
{
    public FriendsController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [Route("")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<UserAccountViewModel>>))]
    public async Task<IActionResult> Index()
    {
        var friends = await _mediator.Send(new GetFriendsFromUserRequest(await GetAuthenticatedUserAccount()));
        return Ok(new GenericResponse(friends.Select(UserAccountViewModel.FromEntity)));
    }

    [HttpDelete]
    [Route("{friendId:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public async Task<IActionResult> Delete([FromRoute] Guid friendId)
    {
        var result = await _mediator.Send(new RemoveFriendRequest(await GetAuthenticatedUserAccount(), friendId));
        if (result.Failed)
        {
            return BadRequest(new ValidationErrorResponse(result.Error));
        }

        // await _hubContext.Clients
        //     .User(friendId.ToString())
        //     .SendAsync(
        //         FriendshipHub.FriendshipRemovedMessage,
        //         FriendshipRequestViewModel.FromEntity(result.Data)
        //     );

        return NoContent();
    }
}