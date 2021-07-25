using System;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Social;
using Agree.Accord.Presentation.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agree.Accord.Presentation.Social.Controllers
{
    [ApiController]
    [Route("api/friends")]
    [Authorize]
    public class FriendsController : CustomControllerBase
    {
        private readonly SocialService _socialService;

        public FriendsController(AccountService accountService, SocialService socialService) : base(accountService)
        {
            _socialService = socialService;
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> SentRequests()
        {
            var friends = await _socialService.GetFriendsFromUserAsync(await GetAuthenticatedUserAccount());
            return Ok(new { Friends = friends.Select(ApplicationUserViewModel.FromEntity) });
        }
    }
}