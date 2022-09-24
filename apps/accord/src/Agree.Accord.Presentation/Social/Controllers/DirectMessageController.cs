namespace Agree.Accord.Presentation.Social.Controllers;

using System;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Social.Dtos;
using Agree.Accord.Domain.Social.Services;
using Agree.Accord.Presentation.Responses;
using Agree.Accord.Presentation.Social.Hubs;
using Agree.Accord.Presentation.Social.ViewModels;
using Agree.Accord.SharedKernel.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[ApiController]
[Route("api")]
[Authorize]
public class DirectMessageController : CustomControllerBase
{
    private readonly DirectMessageService _directMessageService;
    private readonly IHubContext<DirectMessageHub> _hubContext;

    public DirectMessageController(AccountService accountService,
                                   DirectMessageService directMessageService,
                                   IHubContext<DirectMessageHub> hubContext) : base(accountService)
    {
        _directMessageService = directMessageService;
        _hubContext = hubContext;
    }

    [HttpPost]
    [Route("direct-messages")]
    [Authorize]
    public async Task<IActionResult> SendDirectMessage(SendDirectMessageDto sendDirectMessageDto)
    {
        sendDirectMessageDto.From = await GetAuthenticatedUserAccount();
        var result = await _directMessageService.SendDirectMessageAsync(sendDirectMessageDto);
        if (result.Failed)
        {
            return BadRequest(new ValidationErrorResponse(result.Error));
        }
        var vm = DirectMessageViewModel.FromEntity(result.Data);

        await _hubContext.Clients
            .User(result.Data.To.Id.ToString())
            .SendAsync(
                DirectMessageHub.DirectMessageReceivedMessage,
                vm
            );

        var url = Url.Link("GetDirectMessageById", new { id = result.Data.Id });
        return Created(url, new { Message = vm });
    }

    [HttpGet]
    [Route("direct-messages/{id:guid}", Name = "GetDirectMessageById")]
    [Authorize]
    public async Task<IActionResult> Show([FromRoute] Guid id)
    {
        var message = await _directMessageService.GetDirectMessageByIdAsync(id);
        return message == null ? NotFound() : Ok(new { Message = DirectMessageViewModel.FromEntity(message) });
    }

    [HttpGet]
    [Route("friends/{friendId:guid}/direct-messages")]
    [Authorize]
    public async Task<IActionResult> Index([FromRoute] Guid friendId, [FromQuery] Pagination pagination)
    {
        var requester = await GetAuthenticatedUserAccount();
        var messages = await _directMessageService.GetDirectMessagesFromFriendChatAsync(requester.Id, friendId, pagination);
        return Ok(new { Messages = messages.Select(DirectMessageViewModel.FromEntity) });
    }

    [HttpPut]
    [Route("friends/{friendId:guid}/direct-messages")]
    [Authorize]
    public async Task<IActionResult> MarkRead([FromRoute] Guid friendId)
    {
        var requester = await GetAuthenticatedUserAccount();
        var result = await _directMessageService.MarkEntireChatAsRead(requester.Id, friendId);
        return result.Failed
            ? BadRequest(new ValidationErrorResponse(result.Error))
            : Ok(new { Messages = result.Data.Select(DirectMessageViewModel.FromEntity) });
    }
}