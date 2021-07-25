using System;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Social;
using Agree.Accord.Domain.Social.Dtos;
using Agree.Accord.Presentation.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agree.Accord.Presentation.Social.Controllers
{
    [ApiController]
    [Route("api/friendship-requests")]
    [Authorize]
    public class FriendshipRequestController : CustomControllerBase
    {
        private readonly SocialService _socialService;

        public FriendshipRequestController(AccountService accountService, SocialService socialService) : base(accountService)
        {
            _socialService = socialService;
        }

        [HttpGet]
        [Route("sent")]
        [Authorize]
        public async Task<IActionResult> SentRequests()
        {
            var requests = await _socialService.GetSentFriendshipRequestsFromUserAsync(await GetAuthenticatedUserAccount());
            return Ok(requests);
        }

        [HttpGet]
        [Route("received")]
        [Authorize]
        public async Task<IActionResult> ReceivedRequests()
        {
            var requests = await _socialService.GetReceivedFriendshipRequestsFromUserAsync(await GetAuthenticatedUserAccount());
            return Ok(requests);
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
            return Ok();
        }

        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Accept([FromBody] AcceptFriendshipRequestDto acceptFriendshipRequestDto)
        {
            acceptFriendshipRequestDto.LoggedUser = await GetAuthenticatedUserAccount();
            var ok = await _socialService.AcceptFriendshipRequestAsync(acceptFriendshipRequestDto);
            if (ok)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Decline([FromBody] Guid fromUserId)
        {
            var ok = await _socialService.DeclineFriendshipRequestAsync(await GetAuthenticatedUserAccount(), fromUserId);
            if (ok)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}