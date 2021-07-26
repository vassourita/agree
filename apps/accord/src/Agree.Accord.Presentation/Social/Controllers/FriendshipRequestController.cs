using System;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Social;
using Agree.Accord.Domain.Social.Dtos;
using Agree.Accord.Presentation.Responses;
using Agree.Accord.Presentation.Social.Hubs;
using Agree.Accord.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Agree.Accord.Presentation.Social.Controllers
{
    /// <summary>
    /// A controller for managing friendship requests.
    /// </summary>
    [ApiController]
    [Route("api/friendship-requests")]
    [Authorize]
    public class FriendshipRequestController : CustomControllerBase
    {
        private readonly SocialService _socialService;
        private readonly IHubContext<FriendshipHub> _hubContext;

        public FriendshipRequestController(AccountService accountService, SocialService socialService, IHubContext<FriendshipHub> hubContext) : base(accountService)
        {
            _socialService = socialService;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("sent")]
        [Authorize]
        public async Task<IActionResult> SentRequests()
        {
            var requests = await _socialService.GetSentFriendshipRequestsFromUserAsync(await GetAuthenticatedUserAccount());
            return Ok(new { Requests = requests.Select(FriendshipRequestViewModel.FromEntity) });
        }

        [HttpGet]
        [Route("received")]
        [Authorize]
        public async Task<IActionResult> ReceivedRequests()
        {
            var requests = await _socialService.GetReceivedFriendshipRequestsFromUserAsync(await GetAuthenticatedUserAccount());
            return Ok(new { Requests = requests.Select(FriendshipRequestViewModel.FromEntity) });
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Send([FromBody] SendFriendshipRequestDto friendshipRequestDto)
        {
            friendshipRequestDto.From = await GetAuthenticatedUserAccount();
            var result = await _socialService.SendFriendshipRequest(friendshipRequestDto);
            if (result.Failed)
            {
                return BadRequest(new ValidationErrorResponse(result.Error));
            }
            await _hubContext.Clients
                .User(result.Data.ToId.ToString())
                .SendAsync(
                    FriendshipHub.FriendshipRequestReceivedMessage,
                    FriendshipRequestViewModel.FromEntity(result.Data)
                );

            return Ok();
        }

        [HttpPut]
        [Route("{fromUserId:guid}")]
        [Authorize]
        public async Task<IActionResult> Accept([FromRoute] Guid fromUserId)
        {
            var result = await _socialService.AcceptFriendshipRequestAsync(await GetAuthenticatedUserAccount(), fromUserId);
            if (result.Failed)
            {
                return BadRequest();
            }
            await _hubContext.Clients
                .User(fromUserId.ToString())
                .SendAsync(
                    FriendshipHub.FriendshipRequestAcceptedMessage,
                    FriendshipRequestViewModel.FromEntity(result.Data)
                );
            return Ok();
        }

        [HttpDelete]
        [Route("{fromUserId:guid}")]
        [Authorize]
        public async Task<IActionResult> Decline([FromRoute] Guid fromUserId)
        {
            var result = await _socialService.DeclineFriendshipRequestAsync(await GetAuthenticatedUserAccount(), fromUserId);
            if (result.Failed)
            {
                return BadRequest();
            }
            await _hubContext.Clients
                .User(fromUserId.ToString())
                .SendAsync(
                    FriendshipHub.FriendshipRequestDeclinedMessage,
                    FriendshipRequestViewModel.FromEntity(result.Data)
                );
            return Ok();
        }
    }
}