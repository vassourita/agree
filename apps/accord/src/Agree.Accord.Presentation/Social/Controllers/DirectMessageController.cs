using System;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Social.Dtos;
using Agree.Accord.Domain.Social.Services;
using Agree.Accord.Presentation.Responses;
using Agree.Accord.Presentation.Social.Hubs;
using Agree.Accord.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Agree.Accord.Presentation.Social.Controllers
{
    [ApiController]
    [Route("api/direct-messages")]
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
        [Route("")]
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
        [Route("{id}", Name = "GetDirectMessageById")]
        [Authorize]
        public async Task<IActionResult> GetDirectMessage([FromRoute] Guid id)
        {
            var message = await _directMessageService.GetDirectMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(new { Message = DirectMessageViewModel.FromEntity(message) });
        }
    }
}