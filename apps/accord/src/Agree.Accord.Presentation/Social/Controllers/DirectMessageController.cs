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

[ApiController]
[Route("api")]
[Authorize]
public class DirectMessageController : CustomControllerBase
{
    public DirectMessageController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [Route("friends/{friendId:guid}/direct-messages")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<DirectMessageViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
    public async Task<IActionResult> SendDirectMessage([FromBody] SendDirectMessageRequest request, [FromRoute] Guid friendId)
    {
        request.From = await GetAuthenticatedUserAccount();
        request.ToId = friendId;
        var result = await _mediator.Send(request);
        if (result.Failed)
        {
            return BadRequest(new ValidationErrorResponse(result.Error));
        }
        var vm = DirectMessageViewModel.FromEntity(result.Data);

        // await _hubContext.Clients
        //     .User(result.Data.To.Id.ToString())
        //     .SendAsync(
        //         DirectMessageHub.DirectMessageReceivedMessage,
        //         vm
        //     );

        var url = Url.Link("GetDirectMessageById", new { id = result.Data.Id });
        return Created(url, new GenericResponse(vm));
    }

    [HttpGet]
    [Route("direct-messages/{id:guid}", Name = "GetDirectMessageById")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<DirectMessageViewModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Show([FromRoute] GetDirectMessagebyIdRequest request)
    {
        var message = await _mediator.Send(request);
        return message == null ? NotFound() : Ok(new GenericResponse(DirectMessageViewModel.FromEntity(message)));
    }

    [HttpGet]
    [Route("friends/{friendId:guid}/direct-messages")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<DirectMessageViewModel>>))]
    public async Task<IActionResult> Index([FromQuery] GetFriendChatRequest request, [FromRoute] Guid friendId)
    {
        request.FriendId = friendId;
        request.UserId = CurrentlyLoggedUser.Id;
        var messages = await _mediator.Send(request);
        return Ok(new GenericResponse(messages.Select(m => DirectMessageViewModel.FromEntity(m))));
    }

    // [HttpPut]
    // [Route("friends/{friendId:guid}/direct-messages")]
    // [Authorize]
    // public async Task<IActionResult> MarkRead([FromRoute] Guid friendId)
    // {
    //     var requester = await GetAuthenticatedUserAccount();
    //     var result = await _directMessageService.MarkEntireChatAsRead(requester.Id, friendId);
    //     return result.Failed
    //         ? BadRequest(new ValidationErrorResponse(result.Error))
    //         : Ok(new { Messages = result.Data.Select(DirectMessageViewModel.FromEntity) });
    // }
}