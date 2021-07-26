using System;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Social;
using Agree.Accord.Domain.Social.Services;
using Agree.Accord.Presentation.Identity.ViewModels;
using Agree.Accord.Presentation.Responses;
using Agree.Accord.Presentation.Social.Hubs;
using Agree.Accord.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Agree.Accord.Presentation.Social.Controllers
{
    /// <summary>
    /// A controller for managing friends.
    /// </summary>
    [ApiController]
    [Route("api/friends")]
    [Authorize]
    public class FriendsController : CustomControllerBase
    {
        private readonly SocialService _socialService;
        private readonly IHubContext<FriendshipHub> _hubContext;

        public FriendsController(AccountService accountService, SocialService socialService, IHubContext<FriendshipHub> hubContext) : base(accountService)
        {
            _socialService = socialService;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var friends = await _socialService.GetFriendsFromUserAsync(await GetAuthenticatedUserAccount());
            return Ok(new { Friends = friends.Select(ApplicationUserViewModel.FromEntity) });
        }

        [HttpDelete]
        [Route("{friendId:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid friendId)
        {
            var result = await _socialService.RemoveFriend(await GetAuthenticatedUserAccount(), friendId);
            if (result.Failed)
            {
                return BadRequest(new ValidationErrorResponse(result.Error));
            }
            await _hubContext.Clients
                .User(friendId.ToString())
                .SendAsync(
                    FriendshipHub.FriendshipRemovedMessage,
                    FriendshipRequestViewModel.FromEntity(result.Data)
                );
            return NoContent();
        }
    }
}